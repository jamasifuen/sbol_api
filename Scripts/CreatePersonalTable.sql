-- Script para crear la tabla personal
-- Ejecutar este script en tu base de datos SQL Server

USE PersonalDB; -- Cambia por el nombre de tu base de datos
GO

-- Crear la tabla personal si no existe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='personal' AND xtype='U')
BEGIN
    CREATE TABLE personal (
        id INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(100) NOT NULL,
        apellido NVARCHAR(100) NOT NULL,
        documento NVARCHAR(20) NULL,
        cargo NVARCHAR(50) NULL,
        departamento NVARCHAR(50) NULL,
        email NVARCHAR(100) NULL,
        telefono NVARCHAR(20) NULL,
        fecha_ingreso DATETIME2 NULL,
        salario DECIMAL(10,2) NULL,
        activo BIT NOT NULL DEFAULT 1,
        fecha_creacion DATETIME2 NOT NULL DEFAULT GETDATE(),
        fecha_actualizacion DATETIME2 NULL
    );

    -- Crear índices únicos
    CREATE UNIQUE NONCLUSTERED INDEX IX_personal_documento ON personal(documento) WHERE documento IS NOT NULL;
    CREATE UNIQUE NONCLUSTERED INDEX IX_personal_email ON personal(email) WHERE email IS NOT NULL;

    PRINT 'Tabla personal creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla personal ya existe.';
END
GO

-- Insertar datos de ejemplo
IF NOT EXISTS (SELECT * FROM personal)
BEGIN
    INSERT INTO personal (nombre, apellido, documento, cargo, departamento, email, telefono, fecha_ingreso, salario)
    VALUES 
        ('Juan', 'Pérez', '12345678', 'Desarrollador', 'IT', 'juan.perez@empresa.com', '555-0001', '2023-01-15', 50000.00),
        ('María', 'González', '87654321', 'Analista', 'IT', 'maria.gonzalez@empresa.com', '555-0002', '2023-02-01', 45000.00),
        ('Carlos', 'López', '11223344', 'Gerente', 'Ventas', 'carlos.lopez@empresa.com', '555-0003', '2022-06-10', 75000.00),
        ('Ana', 'Martínez', '44332211', 'Asistente', 'RRHH', 'ana.martinez@empresa.com', '555-0004', '2023-03-20', 35000.00),
        ('Luis', 'Rodríguez', '99887766', 'Contador', 'Finanzas', 'luis.rodriguez@empresa.com', '555-0005', '2022-11-05', 55000.00);

    PRINT 'Datos de ejemplo insertados.';
END
ELSE
BEGIN
    PRINT 'La tabla personal ya contiene datos.';
END
GO
