USE dbProyecto;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) UNIQUE NOT NULL,
        Password NVARCHAR(255) NOT NULL, -- SHA256 hash
        Role NVARCHAR(20) NOT NULL DEFAULT 'Usuario' -- 'Admin', 'HR', 'Usuario'
    );
END
GO

-- Insert default Admin with SHA256(admin123)
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, Password, Role) VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin');
END
GO

-- Insert default User with SHA256(user123)
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'user')
BEGIN
    INSERT INTO Users (Username, Password, Role) VALUES ('user', 'e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446', 'Usuario');
END
GO
