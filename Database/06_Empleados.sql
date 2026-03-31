USE dbProyecto;
GO

-- Departamentos Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Departamentos' and xtype='U')
BEGIN
    CREATE TABLE Departamentos (
        Id INT PRIMARY KEY IDENTITY(1,1),
        departamento NVARCHAR(150) NOT NULL
    );
END
GO

-- Empleados Table
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
