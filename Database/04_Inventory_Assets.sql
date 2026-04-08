USE dbProyecto;
GO

-- Tabla Productos (Inventario)
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

-- Movimientos de Inventario (Entradas/Salidas)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StockMovements' and xtype='U')
BEGIN
    CREATE TABLE StockMovements (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ProductId INT NOT NULL,
        MovementDate DATETIME NOT NULL DEFAULT GETDATE(),
        Quantity INT NOT NULL,
        Type NVARCHAR(10) NOT NULL, -- IN / OUT
        Notes NVARCHAR(250) NULL
    );
END
GO
