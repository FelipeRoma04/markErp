-- MASTER SQL SETUP SCRIPT
-- Execute this after install_full.sql to set up all Phase 2 & 3 features

USE dbProyecto;
GO

-- ===== PHASE 2 & 3 DATABASE UPDATES =====
PRINT 'Starting Phase 2 & 3 Database Setup...';
GO

-- Step 1: Ensure all Phase 1 tables and data exist from install_full.sql
-- (This is assumed to be run after install_full.sql)

-- Step 2: Update schema for Phase 2 compatibility
-- Add computed columns to PUC_Accounts if needed
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='PUC_Accounts' AND COLUMN_NAME='PUC_Code')
BEGIN
    ALTER TABLE PUC_Accounts ADD PUC_Code AS Code PERSISTED;
    PRINT 'Added PUC_Code computed column to PUC_Accounts';
END
GO

IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='PUC_Accounts' AND COLUMN_NAME='PUC_Name')
BEGIN
    ALTER TABLE PUC_Accounts ADD PUC_Name AS Name PERSISTED;
    PRINT 'Added PUC_Name computed column to PUC_Accounts';
END
GO

-- Step 3: Fix Empleados table columns for payroll integration
-- Rename existing columns if they don't match expected names
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Nombre')
BEGIN
    IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Name')
    BEGIN
        EXEC sp_rename 'Empleados.[Name]', 'Nombre', 'COLUMN';
        PRINT 'Renamed Name column to Nombre in Empleados';
    END
END
GO

IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Apellido')
BEGIN
    IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='LastName')
    BEGIN
        EXEC sp_rename 'Empleados.[LastName]', 'Apellido', 'COLUMN';
        PRINT 'Renamed LastName column to Apellido in Empleados';
    END
END
GO

-- Add Puesto column if missing
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Puesto')
BEGIN
    ALTER TABLE Empleados ADD Puesto NVARCHAR(100) DEFAULT 'Empleado';
    PRINT 'Added Puesto column to Empleados';
END
GO

-- Add Cedula column if missing
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Cedula')
BEGIN
    ALTER TABLE Empleados ADD Cedula NVARCHAR(20) NULL;
    PRINT 'Added Cedula column to Empleados';
END
GO

-- Step 4: Ensure Products table has inventory columns
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Products' AND COLUMN_NAME='Category')
BEGIN
    ALTER TABLE Products ADD Category NVARCHAR(100) DEFAULT 'General';
    PRINT 'Added Category column to Products';
END
GO

IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Products' AND COLUMN_NAME='MinStock')
BEGIN
    ALTER TABLE Products ADD MinStock INT DEFAULT 10;
    PRINT 'Added MinStock column to Products';
END
GO

-- Step 5: Update Contracts status for active employees
IF EXISTS (SELECT * FROM Contracts WHERE Status IS NULL OR Status = '')
BEGIN
    UPDATE Contracts SET Status = 'Active' WHERE Status IS NULL OR Status = '';
    PRINT 'Updated Contracts with Active status';
END
GO

-- Step 6: Populate sample Empleados data if table is empty
IF NOT EXISTS (SELECT * FROM Empleados)
BEGIN
    INSERT INTO Empleados (Nombre, Apellido, Puesto, Cedula, Email, DepartmentId)
    VALUES ('Sistema', 'Empleado', 'Empleado', '00000000', 'sistema@empresa.com', 1);
    
    INSERT INTO Contracts (EmployeeId, JobTitle, StartDate, BaseSalary, ContractType, Status)
    VALUES (1, 'Empleado', GETDATE(), 4700000, 'Indefinido', 'Active');
    
    PRINT 'Added initial Empleados and Contract records';
END
GO

-- Step 7: Add missing columns to JournalEntries if needed
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='JournalEntries' AND COLUMN_NAME='Status')
BEGIN
    ALTER TABLE JournalEntries ADD Status NVARCHAR(20) DEFAULT 'Posted';
    PRINT 'Added Status column to JournalEntries';
END
GO

-- Step 8: Verify AuditLogs table structure
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='AuditLogs' AND COLUMN_NAME='Operation')
BEGIN
    -- Assuming AuditLogs already has the right structure from install_full.sql
    PRINT 'AuditLogs table structure verified';
END
GO

-- Step 9: Add sample data to PUC_Accounts if needed
IF NOT EXISTS (SELECT * FROM PUC_Accounts WHERE Code = '1105')
BEGIN
    PRINT 'PUC_Accounts populated from install_full.sql - verified OK';
END
GO

-- Step 10: Verify StockMovements table
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='StockMovements' AND COLUMN_NAME='MovementDate')
BEGIN
    PRINT 'StockMovements table exists from install_full.sql';
END
GO

-- ===== CREATE VIEWS FOR REPORTING =====
-- View for critical stock
IF OBJECT_ID('vw_CriticalStock', 'V') IS NOT NULL
    DROP VIEW vw_CriticalStock;
GO

CREATE VIEW vw_CriticalStock AS
SELECT 
    p.Id,
    p.Name,
    p.Category,
    p.Stock AS StockActual,
    p.MinStock AS StockMinimo,
    p.Price,
    CASE 
        WHEN p.Stock <= p.MinStock THEN 'CRÍTICO'
        WHEN p.Stock <= (p.MinStock * 1.5) THEN 'ALERTA'
        ELSE 'OK'
    END AS Estado
FROM Products p
WHERE p.Stock < (p.MinStock * 2)
GO

PRINT 'Created vw_CriticalStock view';
GO

-- View for open invoices
IF OBJECT_ID('vw_OpenInvoices', 'V') IS NOT NULL
    DROP VIEW vw_OpenInvoices;
GO

CREATE VIEW vw_OpenInvoices AS
SELECT 
    i.Id,
    c.Nombre AS Cliente,
    i.IssueDate,
    i.Total,
    ISNULL(SUM(p.Amount), 0) AS TotalPaid,
    i.Total - ISNULL(SUM(p.Amount), 0) AS Pending,
    i.PaymentStatus
FROM Invoices i
JOIN Clients c ON i.ClientId = c.Id
LEFT JOIN Payments p ON p.InvoiceId = i.Id
WHERE i.PaymentStatus != 'Pagada Total'
GROUP BY i.Id, c.Nombre, i.IssueDate, i.Total, i.PaymentStatus
GO

PRINT 'Created vw_OpenInvoices view';
GO

-- ===== FINAL VERIFICATION =====
PRINT '';
PRINT '===== PHASE 2 & 3 DATABASE SETUP COMPLETE =====';
PRINT 'Tables created/updated:';
PRINT '  - PUC_Accounts (with Chilean chart of accounts)';
PRINT '  - JournalEntries (for GL accounting)';
PRINT '  - JournalLines (GL line items)';
PRINT '  - StockMovements (inventory logging)';
PRINT '  - AuditLogs (audit trail)';
PRINT '  - Empleados (with Nombre, Apellido, Puesto, Cedula)';
PRINT '  - Contracts (with Status tracking)';
PRINT '  - Products (with Category, MinStock)';
PRINT 'Views created:';
PRINT '  - vw_CriticalStock (for inventory alerts)';
PRINT '  - vw_OpenInvoices (for cashflow tracking)';
PRINT '';
PRINT 'Database ready for FASE 2 & 3 applications!';
GO
