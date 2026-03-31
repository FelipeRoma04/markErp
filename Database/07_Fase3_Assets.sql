USE dbProyecto;
GO

-- 1. Tabla Ubicaciones / Sedes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Locations' and xtype='U')
BEGIN
    CREATE TABLE Locations (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(150) NOT NULL UNIQUE
    );

    -- Insert Default Location
    INSERT INTO Locations (Name) VALUES ('Sede Principal (HQ)');
END
GO

-- 2. Modificaciones a la Tabla de Activos para Depreciación y Ubicación
IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'PurchasePrice' AND Object_ID = Object_ID(N'Assets'))
BEGIN
    ALTER TABLE Assets ADD PurchasePrice DECIMAL(18,2) NOT NULL DEFAULT 0;
    ALTER TABLE Assets ADD SalvageValue DECIMAL(18,2) NOT NULL DEFAULT 0;
    ALTER TABLE Assets ADD UsefulLifeYears INT NOT NULL DEFAULT 0;
    ALTER TABLE Assets ADD PurchaseDate DATE NOT NULL DEFAULT GETDATE();
    ALTER TABLE Assets ADD LocationId INT NULL; -- Foreign key to Locations
END
GO

-- 3. Tabla de Agenda de Mantenimiento Preventivo
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MaintenanceSchedule' and xtype='U')
BEGIN
    CREATE TABLE MaintenanceSchedule (
        Id INT PRIMARY KEY IDENTITY(1,1),
        AssetId INT NOT NULL,
        Description NVARCHAR(250) NOT NULL,
        ScheduledDate DATE NOT NULL,
        IsCompleted BIT NOT NULL DEFAULT 0,
        CompletedDate DATE NULL,
        Cost DECIMAL(18,2) NULL,
        Notes NVARCHAR(500) NULL
    );
END
GO
