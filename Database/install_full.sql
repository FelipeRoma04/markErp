-- install_full.sql
-- Script maestro para crear la base de datos dbProyecto con datos iniciales.
-- Ejecuta este archivo con:
--   sqlcmd -S localhost\SQLEXPRESS -i install_full.sql

IF DB_ID('dbProyecto') IS NULL
BEGIN
    CREATE DATABASE dbProyecto;
END
GO

USE dbProyecto;
GO

/* Usuarios y roles */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) UNIQUE NOT NULL,
        Password NVARCHAR(255) NOT NULL,
        Role NVARCHAR(20) NOT NULL DEFAULT 'Usuario'
    );
END
GO

IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, Password, Role)
    VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin');
END
GO

/* Departamentos y empleados */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Departamentos' and xtype='U')
BEGIN
    CREATE TABLE Departamentos (
        Id INT PRIMARY KEY IDENTITY(1,1),
        departamento NVARCHAR(150) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM Departamentos WHERE departamento = 'General')
BEGIN
    INSERT INTO Departamentos (departamento) VALUES ('General');
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Empleados' and xtype='U')
BEGIN
    CREATE TABLE Empleados (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        SecondName NVARCHAR(100) NULL,
        Email NVARCHAR(150) NULL,
        DepartmentId INT NULL,
        PicFoto VARBINARY(MAX) NULL
    );

    ALTER TABLE Empleados
    WITH CHECK ADD CONSTRAINT FK_Empleados_Departamentos
    FOREIGN KEY (DepartmentId) REFERENCES Departamentos(Id);
END
GO

/* HR / Payroll */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Contracts' and xtype='U')
BEGIN
    CREATE TABLE Contracts (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EmployeeId INT NOT NULL,
        JobTitle NVARCHAR(100) NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NULL,
        BaseSalary DECIMAL(18,2) NOT NULL,
        ContractType NVARCHAR(50) NOT NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'Active'
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Attendance' and xtype='U')
BEGIN
    CREATE TABLE Attendance (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EmployeeId INT NOT NULL,
        WorkDate DATE NOT NULL,
        HoursWorked DECIMAL(5,2) NOT NULL,
        OvertimeHours DECIMAL(5,2) DEFAULT 0,
        IsAbsent BIT DEFAULT 0,
        Notes NVARCHAR(255) NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payroll_Log' and xtype='U')
BEGIN
    CREATE TABLE Payroll_Log (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EmployeeId INT NOT NULL,
        PayPeriodStart DATE NOT NULL,
        PayPeriodEnd DATE NOT NULL,
        GrossPay DECIMAL(18,2) NOT NULL,
        Deductions DECIMAL(18,2) NOT NULL,
        NetPay DECIMAL(18,2) NOT NULL,
        PaymentDate DATE NOT NULL
    );
END
GO

/* Clientes y ventas */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clients' and xtype='U')
BEGIN
    CREATE TABLE Clients (
        Id INT PRIMARY KEY IDENTITY(1,1),
        DocumentID NVARCHAR(20) UNIQUE NOT NULL,
        Name NVARCHAR(150) NOT NULL,
        Email NVARCHAR(100) NULL,
        Phone NVARCHAR(50) NULL,
        Address NVARCHAR(250) NULL,
        City NVARCHAR(100) NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Quotes' and xtype='U')
BEGIN
    CREATE TABLE Quotes (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ClientId INT NOT NULL,
        IssueDate DATE NOT NULL,
        ExpirationDate DATE NOT NULL,
        TotalAmount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(30) DEFAULT 'Pendiente'
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Invoices' and xtype='U')
BEGIN
    CREATE TABLE Invoices (
        Id INT PRIMARY KEY IDENTITY(1,1),
        InvoiceNumber INT UNIQUE NULL,
        QuoteId INT NULL,
        ClientId INT NOT NULL,
        IssueDate DATE NOT NULL,
        DueDate DATE NOT NULL,
        Subtotal DECIMAL(18,2) NOT NULL,
        TotalTax DECIMAL(18,2) NOT NULL,
        ReteFuente DECIMAL(18,2) NOT NULL DEFAULT 0,
        Total DECIMAL(18,2) NOT NULL,
        PaymentStatus NVARCHAR(30) DEFAULT 'Por Cobrar'
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'InvoiceNumberSeq')
BEGIN
    CREATE SEQUENCE InvoiceNumberSeq AS INT START WITH 1001 INCREMENT BY 1;
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' and xtype='U')
BEGIN
    CREATE TABLE Payments (
        Id INT PRIMARY KEY IDENTITY(1,1),
        InvoiceId INT NOT NULL,
        PaymentDate DATE NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        Method NVARCHAR(50) NOT NULL
    );
END
GO

/* Inventario */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' and xtype='U')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Code NVARCHAR(50) UNIQUE NOT NULL,
        Name NVARCHAR(150) NOT NULL,
        CostPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
        SalePrice DECIMAL(18,2) NOT NULL DEFAULT 0,
        Stock INT NOT NULL DEFAULT 0,
        MinStock INT NOT NULL DEFAULT 5
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StockMovements' and xtype='U')
BEGIN
    CREATE TABLE StockMovements (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ProductId INT NOT NULL,
        MovementDate DATETIME NOT NULL DEFAULT GETDATE(),
        Quantity INT NOT NULL,
        Type NVARCHAR(10) NOT NULL,
        Notes NVARCHAR(250) NULL
    );
END
GO

/* Ubicaciones / Sedes */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Locations' and xtype='U')
BEGIN
    CREATE TABLE Locations (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(150) NOT NULL UNIQUE
    );

    INSERT INTO Locations (Name) VALUES ('Sede Principal (HQ)');
END
GO

/* Activos */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Assets' and xtype='U')
BEGIN
    CREATE TABLE Assets (
        Id INT PRIMARY KEY IDENTITY(1,1),
        SerialNumber NVARCHAR(100) UNIQUE NOT NULL,
        Type NVARCHAR(100) NOT NULL,
        Brand NVARCHAR(100) NULL,
        Status NVARCHAR(50) DEFAULT 'Available',
        Location NVARCHAR(150) NULL,
        PurchaseDate DATE NOT NULL DEFAULT GETDATE(),
        PurchasePrice DECIMAL(18,2) NOT NULL DEFAULT 0,
        SalvageValue DECIMAL(18,2) NOT NULL DEFAULT 0,
        UsefulLifeYears INT NOT NULL DEFAULT 0,
        LocationId INT NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AssetAssignments' and xtype='U')
BEGIN
    CREATE TABLE AssetAssignments (
        Id INT PRIMARY KEY IDENTITY(1,1),
        AssetId INT NOT NULL,
        EmployeeId INT NOT NULL,
        AssignDate DATE NOT NULL,
        ReturnDate DATE NULL,
        Notes NVARCHAR(250) NULL
    );
END
GO

/* Auditoría y PUC */
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AuditLogs' and xtype='U')
BEGIN
    CREATE TABLE AuditLogs (
        Id INT PRIMARY KEY IDENTITY(1,1),
        UserLogin NVARCHAR(100) NOT NULL DEFAULT 'sistema',
        ActionType NVARCHAR(50) NOT NULL,
        TableName NVARCHAR(100) NOT NULL,
        RecordId NVARCHAR(100) NULL,
        Description NVARCHAR(500) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PUC_Accounts' and xtype='U')
BEGIN
    CREATE TABLE PUC_Accounts (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Code NVARCHAR(20) NOT NULL UNIQUE,
        Name NVARCHAR(200) NOT NULL,
        AccountType NVARCHAR(50) NOT NULL,
        Nature NVARCHAR(10) NOT NULL
    );

    INSERT INTO PUC_Accounts (Code, Name, AccountType, Nature) VALUES
        ('1','ACTIVOS','ACTIVO','DEBITO'),
        ('11','Efectivo y equivalentes de efectivo','ACTIVO','DEBITO'),
        ('1105','Caja General','ACTIVO','DEBITO'),
        ('1110','Bancos','ACTIVO','DEBITO'),
        ('13','Deudores Comerciales','ACTIVO','DEBITO'),
        ('1305','Clientes','ACTIVO','DEBITO'),
        ('15','Propiedades, planta y equipo','ACTIVO','DEBITO'),
        ('1524','Equipo de Oficina','ACTIVO','DEBITO'),
        ('2','PASIVOS','PASIVO','CREDITO'),
        ('21','Obligaciones Financieras','PASIVO','CREDITO'),
        ('22','Proveedores Nacionales','PASIVO','CREDITO'),
        ('24','Impuestos por Pagar','PASIVO','CREDITO'),
        ('2408','IVA por Pagar','PASIVO','CREDITO'),
        ('3','PATRIMONIO','PATRIMONIO','CREDITO'),
        ('31','Capital Social','PATRIMONIO','CREDITO'),
        ('36','Resultados del Ejercicio','PATRIMONIO','CREDITO'),
        ('4','INGRESOS','INGRESO','CREDITO'),
        ('41','Ingresos Operacionales','INGRESO','CREDITO'),
        ('4135','Ingresos por Ventas','INGRESO','CREDITO'),
        ('5','GASTOS','GASTO','DEBITO'),
        ('51','Gastos Operacionales de Admón','GASTO','DEBITO'),
        ('5105','Gastos de Personal','GASTO','DEBITO'),
        ('5110','Honorarios','GASTO','DEBITO'),
        ('6','COSTOS','COSTO','DEBITO'),
        ('61','Costo de Ventas','COSTO','DEBITO');
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='JournalEntries' and xtype='U')
BEGIN
    CREATE TABLE JournalEntries (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EntryDate DATE NOT NULL,
        Description NVARCHAR(300) NOT NULL,
        CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'sistema',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='JournalLines' and xtype='U')
BEGIN
    CREATE TABLE JournalLines (
        Id INT PRIMARY KEY IDENTITY(1,1),
        JournalEntryId INT NOT NULL,
        AccountId INT NOT NULL,
        Debit DECIMAL(18,2) NOT NULL DEFAULT 0,
        Credit DECIMAL(18,2) NOT NULL DEFAULT 0
    );
END
GO

PRINT 'Instalación base completada.';
