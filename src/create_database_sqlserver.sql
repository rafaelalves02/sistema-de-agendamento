USE sistema_de_agendamento;
GO

-- Tabela: role
IF OBJECT_ID(N'dbo.role', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.role (
        role_id INT NOT NULL IDENTITY(1,1),
        name VARCHAR(45) NOT NULL,
        CONSTRAINT PK_role PRIMARY KEY (role_id)
    );
END
GO

-- Tabela: user
IF OBJECT_ID(N'dbo.[user]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[user] (
        user_id INT NOT NULL IDENTITY(1,1),
        username VARCHAR(100) NOT NULL,
        [password] VARCHAR(100) NOT NULL,
        role_id INT NOT NULL,
        CONSTRAINT PK_user PRIMARY KEY (user_id),
        CONSTRAINT FK_user_role FOREIGN KEY (role_id)
            REFERENCES dbo.role(role_id)
    );
END
GO

-- Tabela: employee
IF OBJECT_ID(N'dbo.employee', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.employee (
        employee_id INT NOT NULL IDENTITY(1,1),
        name VARCHAR(100) NOT NULL,
        phone_number VARCHAR(25) NOT NULL,
        email VARCHAR(100) NULL,
        user_id INT NOT NULL,
        CONSTRAINT PK_employee PRIMARY KEY (employee_id),
        CONSTRAINT FK_employee_user FOREIGN KEY (user_id)
            REFERENCES dbo.[user](user_id)
    );
END
GO

-- Tabela: availability
IF OBJECT_ID(N'dbo.availability', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.availability (
        availability_id INT NOT NULL IDENTITY(1,1),
        weekday VARCHAR(9) NOT NULL CHECK (weekday IN ('monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday')),
        start_time TIME NULL,
        end_time TIME NULL,
        is_active TINYINT NOT NULL DEFAULT 1, -- igual ao MySQL (TINYINT)
        employee_id INT NOT NULL,
        CONSTRAINT PK_availability PRIMARY KEY (availability_id),
        CONSTRAINT FK_availability_employee FOREIGN KEY (employee_id)
            REFERENCES dbo.employee(employee_id)
    );
END
GO

-- Tabela: client
IF OBJECT_ID(N'dbo.client', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.client (
        client_id INT NOT NULL IDENTITY(1,1),
        name VARCHAR(100) NOT NULL,
        phone_number VARCHAR(25) NOT NULL,
        CONSTRAINT PK_client PRIMARY KEY (client_id)
    );
END
GO

-- Tabela: service
IF OBJECT_ID(N'dbo.service', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.service (
        service_id INT NOT NULL IDENTITY(1,1),
        name VARCHAR(45) NOT NULL,
        price DECIMAL(10,2) NOT NULL,
        duration INT NOT NULL,
        CONSTRAINT PK_service PRIMARY KEY (service_id)
    );
END
GO

-- Tabela: appointment
IF OBJECT_ID(N'dbo.appointment', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.appointment (
        appointment_id INT NOT NULL IDENTITY(1,1),
        client_id INT NOT NULL,
        service_id INT NOT NULL,
        status VARCHAR(10) NOT NULL CHECK (status IN ('scheduled', 'canceled', 'completed')),
        created_at DATETIME NOT NULL DEFAULT GETDATE(), -- igual ao NOW() no MySQL
        start_time DATETIME NOT NULL,
        end_time DATETIME NOT NULL,
        employee_id INT NOT NULL,
        CONSTRAINT PK_appointment PRIMARY KEY (appointment_id),
        CONSTRAINT FK_appointment_client FOREIGN KEY (client_id)
            REFERENCES dbo.client(client_id),
        CONSTRAINT FK_appointment_employee FOREIGN KEY (employee_id)
            REFERENCES dbo.employee(employee_id),
        CONSTRAINT FK_appointment_service FOREIGN KEY (service_id)
            REFERENCES dbo.service(service_id)
    );
END
GO

-- Inserir dados iniciais em role
IF NOT EXISTS (SELECT 1 FROM dbo.role WHERE name = 'admin')
BEGIN
    INSERT INTO dbo.role (name) VALUES ('admin');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.role WHERE name = 'employee')
BEGIN
    INSERT INTO dbo.role (name) VALUES ('employee');
END
GO

-- Inserir usuário admin padrão
IF NOT EXISTS (SELECT 1 FROM dbo.[user] WHERE username = 'admin')
BEGIN
    INSERT INTO dbo.[user] (username, [password], role_id)
    VALUES ('admin', '123', 1);
END
GO