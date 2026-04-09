-- FASE 4 - Task 37: Company Settings Table
-- Stores singleton company configuration for invoices, payroll slips, reports

USE dbProyecto;
GO

-- Create Company Settings Table (Singleton pattern - only one company record)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Company]
    (
        [Id] INT PRIMARY KEY DEFAULT 1,
        [CompanyName] NVARCHAR(200) NOT NULL,
        [Nit] NVARCHAR(20) NOT NULL UNIQUE,
        [Address] NVARCHAR(500),
        [City] NVARCHAR(100),
        [Phone] NVARCHAR(20),
        [Email] NVARCHAR(100),
        [LogoPath] NVARCHAR(MAX),
        [CreatedDate] DATETIME DEFAULT GETDATE(),
        [UpdatedDate] DATETIME DEFAULT GETDATE(),
        CONSTRAINT CK_Company_Id CHECK (Id = 1)  -- Enforce singleton pattern
    );
    
    PRINT 'Table [Company] created successfully';
END
ELSE
BEGIN
    PRINT 'Table [Company] already exists';
END;

-- Create index on NIT for quick lookups
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Company_Nit')
BEGIN
    CREATE UNIQUE INDEX IX_Company_Nit ON [dbo].[Company]([Nit]);
    PRINT 'Index IX_Company_Nit created';
END;

-- Insert default company record
IF NOT EXISTS (SELECT * FROM [dbo].[Company] WHERE Id = 1)
BEGIN
    INSERT INTO [dbo].[Company] (Id, CompanyName, Nit, Address, City, Phone, Email)
    VALUES (1, 'Mi Empresa', '0000000000', 'Dirección', 'Ciudad', '', '');
    
    PRINT 'Default company record inserted';
END;

GO

-- Verification query
SELECT 'Company table setup complete. Company records:' as Status;
SELECT * FROM [dbo].[Company];
