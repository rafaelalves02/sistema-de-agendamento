-- Criar o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'sistema_de_agendamento')
BEGIN
    CREATE DATABASE sistema_de_agendamento;
END
GO

USE sistema_de_agendamento;
GO

-- Criar tabela role
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[role]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[role] (
        [role_id] INT NOT NULL IDENTITY(1,1),
        [name] VARCHAR(45) NOT NULL,
        PRIMARY KEY ([role_id])
    );
END
GO

-- Criar tabela user
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[user] (
        [user_id] INT NOT NULL IDENTITY(1,1),
        [username] VARCHAR(100) NOT NULL,
        [password] VARCHAR(100) NOT NULL,
        [role_id] INT NOT NULL,
        PRIMARY KEY ([user_id]),
        CONSTRAINT [user_role]
            FOREIGN KEY ([role_id])
            REFERENCES [dbo].[role] ([role_id])
    );
END
GO

-- Criar tabela employee
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[employee]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[employee] (
        [employee_id] INT NOT NULL IDENTITY(1,1),
        [name] VARCHAR(100) NOT NULL,
        [phone_number] VARCHAR(25) NOT NULL,
        [email] VARCHAR(100) NULL,
        [user_id] INT NOT NULL,
        PRIMARY KEY ([employee_id]),
        CONSTRAINT [employee_user]
            FOREIGN KEY ([user_id])
            REFERENCES [dbo].[user] ([user_id])
    );
END
GO

-- Criar tabela availability
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[availability]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[availability] (
        [availability_id] INT NOT NULL IDENTITY(1,1),
        [weekday] VARCHAR(10) NOT NULL CHECK ([weekday] IN ('monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday')),
        [start_time] TIME NULL,
        [end_time] TIME NULL,
        [is_active] BIT NULL DEFAULT 1,
        [employee_id] INT NOT NULL,
        PRIMARY KEY ([availability_id]),
        CONSTRAINT [availability_employee]
            FOREIGN KEY ([employee_id])
            REFERENCES [dbo].[employee] ([employee_id])
    );
END
GO

-- Criar tabela client
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[client]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[client] (
        [client_id] INT NOT NULL IDENTITY(1,1),
        [name] VARCHAR(100) NOT NULL,
        [phone_number] VARCHAR(25) NOT NULL,
        PRIMARY KEY ([client_id])
    );
END
GO

-- Criar tabela service
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[service]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[service] (
        [service_id] INT NOT NULL IDENTITY(1,1),
        [name] VARCHAR(45) NOT NULL,
        [price] DECIMAL(10,2) NOT NULL,
        [duration] INT NOT NULL,
        PRIMARY KEY ([service_id])
    );
END
GO

-- Criar tabela appointment
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[appointment]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[appointment] (
        [appointment_id] INT NOT NULL IDENTITY(1,1),
        [client_id] INT NOT NULL,
        [service_id] INT NOT NULL,
        [status] VARCHAR(10) NOT NULL CHECK ([status] IN ('scheduled', 'canceled', 'completed')),
        [created_at] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [start_time] DATETIME2 NOT NULL,
        [end_time] DATETIME2 NOT NULL,
        [employee_id] INT NOT NULL,
        PRIMARY KEY ([appointment_id]),
        CONSTRAINT [appointment_client]
            FOREIGN KEY ([client_id])
            REFERENCES [dbo].[client] ([client_id]),
        CONSTRAINT [appointment_employee]
            FOREIGN KEY ([employee_id])
            REFERENCES [dbo].[employee] ([employee_id]),
        CONSTRAINT [appointment_service]
            FOREIGN KEY ([service_id])
            REFERENCES [dbo].[service] ([service_id])
    );
END
GO

-- Inserir dados iniciais na tabela role
IF NOT EXISTS (SELECT * FROM [dbo].[role] WHERE [name] = 'admin')
BEGIN
    INSERT INTO [dbo].[role] ([name]) VALUES ('admin');
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[role] WHERE [name] = 'employee')
BEGIN
    INSERT INTO [dbo].[role] ([name]) VALUES ('employee');
END
GO

-- Inserir usuário admin inicial
IF NOT EXISTS (SELECT * FROM [dbo].[user] WHERE [username] = 'admin')
BEGIN
    INSERT INTO [dbo].[user] ([username], [password], [role_id]) 
    VALUES ('admin', '123', 1);
END
GO

