USE dbProyecto;
GO

-- Tabla Productos (Inventario)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' and xtype='U')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Code NVARCHAR(50) UNIQUE NOT NULL,
        Name NVARCHAR(150) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        Stock INT NOT NULL DEFAULT 0,
        MinStock INT NOT NULL DEFAULT 5
    );
END
GO
