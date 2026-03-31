USE dbProyecto;
GO

-- Tabla Clientes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clients' and xtype='U')
BEGIN
    CREATE TABLE Clients (
        Id INT PRIMARY KEY IDENTITY(1,1),
        DocumentID NVARCHAR(20) UNIQUE NOT NULL, -- NIT o CC
        Name NVARCHAR(150) NOT NULL,
        Email NVARCHAR(100) NULL,
        Phone NVARCHAR(50) NULL,
        Address NVARCHAR(250) NULL
    );
END
GO

-- Tabla Cotizaciones
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Quotes' and xtype='U')
BEGIN
    CREATE TABLE Quotes (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ClientId INT NOT NULL,
        IssueDate DATE NOT NULL,
        ExpirationDate DATE NOT NULL,
        TotalAmount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(30) DEFAULT 'Pendiente' -- Pendiente, Aceptada, Rechazada
    );
END
GO

-- Tabla Facturas
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Invoices' and xtype='U')
BEGIN
    CREATE TABLE Invoices (
        Id INT PRIMARY KEY IDENTITY(1,1),
        QuoteId INT NULL,
        ClientId INT NOT NULL,
        IssueDate DATE NOT NULL,
        DueDate DATE NOT NULL,
        Subtotal DECIMAL(18,2) NOT NULL,
        TotalTax DECIMAL(18,2) NOT NULL, -- Ej: IVA 19%
        Total DECIMAL(18,2) NOT NULL,
        PaymentStatus NVARCHAR(30) DEFAULT 'Por Cobrar' -- Por Cobrar, Pagada Parcial, Pagada Total
    );
END
GO

-- Tabla Pagos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' and xtype='U')
BEGIN
    CREATE TABLE Payments (
        Id INT PRIMARY KEY IDENTITY(1,1),
        InvoiceId INT NOT NULL,
        PaymentDate DATE NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        Method NVARCHAR(50) NOT NULL -- Efectivo, Transferencia, Tarjeta
    );
END
GO
