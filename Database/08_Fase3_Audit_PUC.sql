USE dbProyecto;
GO

-- =============================================
-- AUDITORÍA — Historial de cambios (#20)
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AuditLogs' and xtype='U')
BEGIN
    CREATE TABLE AuditLogs (
        Id          INT PRIMARY KEY IDENTITY(1,1),
        UserLogin   NVARCHAR(100) NOT NULL DEFAULT 'sistema',
        ActionType  NVARCHAR(50)  NOT NULL, -- CREATE | UPDATE | DELETE
        TableName   NVARCHAR(100) NOT NULL,
        RecordId    NVARCHAR(100) NULL,
        Description NVARCHAR(500) NULL,
        CreatedAt   DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- CONTABILIDAD PUC COLOMBIANO (#17)
-- =============================================

-- Catálogo PUC (Plan Único de Cuentas)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PUC_Accounts' and xtype='U')
BEGIN
    CREATE TABLE PUC_Accounts (
        Id          INT PRIMARY KEY IDENTITY(1,1),
        Code        NVARCHAR(20)  NOT NULL UNIQUE,
        Name        NVARCHAR(200) NOT NULL,
        AccountType NVARCHAR(50)  NOT NULL, -- ACTIVO | PASIVO | PATRIMONIO | INGRESO | GASTO | COSTO
        Nature      NVARCHAR(10)  NOT NULL  -- DEBITO | CREDITO
    );

    -- Seed with top-level PUC colombiano accounts
    INSERT INTO PUC_Accounts (Code, Name, AccountType, Nature) VALUES
        ('1',    'ACTIVOS',                            'ACTIVO',     'DEBITO'),
        ('11',   'Efectivo y equivalentes de efectivo','ACTIVO',     'DEBITO'),
        ('1105', 'Caja General',                       'ACTIVO',     'DEBITO'),
        ('1110', 'Bancos',                             'ACTIVO',     'DEBITO'),
        ('13',   'Deudores Comerciales',               'ACTIVO',     'DEBITO'),
        ('1305', 'Clientes',                           'ACTIVO',     'DEBITO'),
        ('15',   'Propiedades, planta y equipo',       'ACTIVO',     'DEBITO'),
        ('1524', 'Equipo de Oficina',                  'ACTIVO',     'DEBITO'),
        ('2',    'PASIVOS',                            'PASIVO',     'CREDITO'),
        ('21',   'Obligaciones Financieras',           'PASIVO',     'CREDITO'),
        ('22',   'Proveedores Nacionales',             'PASIVO',     'CREDITO'),
        ('24',   'Impuestos por Pagar',                'PASIVO',     'CREDITO'),
        ('2408', 'IVA por Pagar',                      'PASIVO',     'CREDITO'),
        ('3',    'PATRIMONIO',                         'PATRIMONIO', 'CREDITO'),
        ('31',   'Capital Social',                     'PATRIMONIO', 'CREDITO'),
        ('36',   'Resultados del Ejercicio',           'PATRIMONIO', 'CREDITO'),
        ('4',    'INGRESOS',                           'INGRESO',    'CREDITO'),
        ('41',   'Ingresos Operacionales',             'INGRESO',    'CREDITO'),
        ('4135', 'Ingresos por Ventas',                'INGRESO',    'CREDITO'),
        ('5',    'GASTOS',                             'GASTO',      'DEBITO'),
        ('51',   'Gastos Operacionales de Admón',      'GASTO',      'DEBITO'),
        ('5105', 'Gastos de Personal',                 'GASTO',      'DEBITO'),
        ('5110', 'Honorarios',                         'GASTO',      'DEBITO'),
        ('6',    'COSTOS',                             'COSTO',      'DEBITO'),
        ('61',   'Costo de Ventas',                    'COSTO',      'DEBITO');
END
GO

-- Libro Diario (Journal Entries)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='JournalEntries' and xtype='U')
BEGIN
    CREATE TABLE JournalEntries (
        Id          INT PRIMARY KEY IDENTITY(1,1),
        EntryDate   DATE NOT NULL,
        Description NVARCHAR(300) NOT NULL,
        CreatedBy   NVARCHAR(100) NOT NULL DEFAULT 'sistema',
        CreatedAt   DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- Líneas del Asiento (Journal Lines)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='JournalLines' and xtype='U')
BEGIN
    CREATE TABLE JournalLines (
        Id            INT PRIMARY KEY IDENTITY(1,1),
        JournalEntryId INT NOT NULL,
        AccountId     INT NOT NULL,
        Debit         DECIMAL(18,2) NOT NULL DEFAULT 0,
        Credit        DECIMAL(18,2) NOT NULL DEFAULT 0
    );
END
GO
