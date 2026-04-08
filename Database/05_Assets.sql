USE dbProyecto;
GO

-- Tabla de Activos Físicos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Assets' and xtype='U')
BEGIN
    CREATE TABLE Assets (
        Id INT PRIMARY KEY IDENTITY(1,1),
        SerialNumber NVARCHAR(100) UNIQUE NOT NULL,
        Type NVARCHAR(100) NOT NULL, -- Laptop, Mobile, Vehicle
        Brand NVARCHAR(100) NULL,
        Status NVARCHAR(50) DEFAULT 'Available', -- Available, Assigned, Maintenance, Retired
        Location NVARCHAR(150) NULL,
        PurchaseDate DATE NULL,
        PurchaseValue DECIMAL(18,2) NULL,
        DepreciationMonths INT NULL,
        ResidualValue DECIMAL(18,2) NULL
    );
END
GO

-- Historial de Asignaciones
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
