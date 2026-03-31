USE dbProyecto;
GO

-- Employee Contracts Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Contracts' and xtype='U')
BEGIN
    CREATE TABLE Contracts (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EmployeeId INT NOT NULL,
        JobTitle NVARCHAR(100) NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NULL,
        BaseSalary DECIMAL(18,2) NOT NULL,
        ContractType NVARCHAR(50) NOT NULL, -- e.g., 'Permanent', 'Fixed Term'
        Status NVARCHAR(20) NOT NULL DEFAULT 'Active'
    );
END
GO

-- Attendance Table
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

-- Payroll Log Table
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
