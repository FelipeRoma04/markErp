-- Updates for Phase 2 & 3 integration
-- Add missing columns and fix schema

USE dbProyecto;
GO

-- Update JournalEntries to store PUC account info if needed
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='JournalEntries' AND COLUMN_NAME='Status')
BEGIN
    ALTER TABLE JournalEntries ADD Status NVARCHAR(20) DEFAULT 'Posted';
END
GO

-- Update PUC_Accounts to have both Code and PUC_Code (for compatibility)
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='PUC_Accounts' AND COLUMN_NAME='PUC_Code')
BEGIN
    ALTER TABLE PUC_Accounts ADD PUC_Code AS Code PERSISTED;
END
GO

-- Update PUC_Accounts to have PUC_Name
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='PUC_Accounts' AND COLUMN_NAME='PUC_Name')
BEGIN
    ALTER TABLE PUC_Accounts ADD PUC_Name AS Name PERSISTED;
END
GO

-- Verify Empleados table has all needed columns
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Nombre')
BEGIN
    -- Rename if Name exists, otherwise add Nombre
    IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Name')
    BEGIN
        EXEC sp_rename 'Empleados.Name', 'Nombre', 'COLUMN';
    END
    ELSE
    BEGIN
        ALTER TABLE Empleados ADD Nombre NVARCHAR(100);
    END
END
GO

-- Verify Empleados has Apellido
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Apellido')
BEGIN
    IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='LastName')
    BEGIN
        EXEC sp_rename 'Empleados.LastName', 'Apellido', 'COLUMN';
    END
    ELSE
    BEGIN
        ALTER TABLE Empleados ADD Apellido NVARCHAR(100);
    END
END
GO

-- Verify Empleados has Puesto
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Puesto')
BEGIN
    IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='JobTitle')
    BEGIN
        -- Query current contracts to populate
        UPDATE e SET Puesto = 'Empleado' 
        FROM Empleados e 
        WHERE Puesto IS NULL;
        
        ALTER TABLE Empleados ADD Puesto NVARCHAR(100) DEFAULT 'Empleado';
    END
    ELSE
    BEGIN
        ALTER TABLE Empleados ADD Puesto NVARCHAR(100) DEFAULT 'Empleado';
    END
END
GO

-- Verify Empleados has Cedula
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Empleados' AND COLUMN_NAME='Cedula')
BEGIN
    ALTER TABLE Empleados ADD Cedula NVARCHAR(20);
END
GO

-- Ensure Products table has all needed columns for inventory reports
IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Products' AND COLUMN_NAME='Category')
BEGIN
    ALTER TABLE Products ADD Category NVARCHAR(100) DEFAULT 'General';
END
GO

IF NOT EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME='Products' AND COLUMN_NAME='MinStock')
BEGIN
    ALTER TABLE Products ADD MinStock INT DEFAULT 10;
END
GO

-- Update Contracts to have Status = 'Active' for all active employees
UPDATE Contracts SET Status = 'Active' 
WHERE Status IS NULL AND EndDate IS NULL;
GO

-- Sample data: Add Nombre/Apellido to Empleados if empty
UPDATE e SET Nombre = 'Sistema', Apellido = 'Empleado'
FROM Empleados e
WHERE (Nombre IS NULL OR Nombre = '') 
  AND (Apellido IS NULL OR Apellido = '');
GO

PRINT 'Phase 2 & 3 Integration Schema Updates Completed.';
