# API Personal SIMAI

Una API REST completa para la gestión de personal desarrollada con .NET Core y Entity Framework Core.

## Características

- ✅ CRUD completo para gestión de personal
- ✅ Arquitectura en capas con servicios y repositorios
- ✅ Entity Framework Core con SQL Server
- ✅ Swagger UI para documentación interactiva
- ✅ Logging y manejo de errores
- ✅ Endpoints para consultas específicas (por departamento, activos)

## Estructura del Proyecto

```
PersonalAPI/
├── Controllers/        # Controladores de la API
├── Models/            # Modelos de datos
├── Data/              # Contexto de Entity Framework
├── Services/          # Lógica de negocio
├── Scripts/           # Scripts SQL
└── README.md
```

## Tecnologías Utilizadas

- .NET Core 8.0
- Entity Framework Core 9.0
- SQL Server
- Swagger/OpenAPI

## Configuración

### 1. Base de Datos

Ejecuta el script SQL incluido en `Scripts/CreatePersonalTable.sql` para crear la tabla:

```sql
USE tu_base_de_datos;
-- Ejecutar el contenido del archivo CreatePersonalTable.sql
```

### 2. Cadena de Conexión

Actualiza la cadena de conexión en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu_servidor;Database=tu_base_datos;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 3. Ejecución

```bash
dotnet run
```

La API estará disponible en:
- HTTPS: `https://localhost:7000`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:7000/swagger`

## Endpoints de la API

### Autenticación

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| POST | `/api/auth/login` | Obtener token JWT | ❌ No requerida |
| GET | `/api/auth/verify` | Verificar token actual | ✅ Bearer Token |
| GET | `/api/auth/users` | Usuarios de ejemplo | ❌ No requerida |

### Personal (🔒 Requiere Autenticación JWT)

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| GET | `/api/personal` | Obtener todo el personal | ✅ Bearer Token |
| GET | `/api/personal/{id}` | Obtener personal por ID | ✅ Bearer Token |
| POST | `/api/personal` | Crear nuevo personal | ✅ Bearer Token |
| PUT | `/api/personal/{id}` | Actualizar personal | ✅ Bearer Token |
| DELETE | `/api/personal/{id}` | Eliminar personal | ✅ Bearer Token |
| GET | `/api/personal/departamento/{departamento}` | Obtener personal por departamento | ✅ Bearer Token |
| GET | `/api/personal/activos` | Obtener solo personal activo | ✅ Bearer Token |

### Ejemplos de Uso

#### Crear Personal (POST)
```json
{
  "nombre": "Roberto",
  "apellido": "Silva",
  "documento": "55555555",
  "cargo": "Programador",
  "departamento": "IT",
  "email": "roberto.silva@empresa.com",
  "telefono": "555-0006",
  "fechaIngreso": "2024-01-15T00:00:00",
  "salario": 48000.00,
  "activo": true
}
```

#### Actualizar Personal (PUT)
```json
{
  "nombre": "Roberto",
  "apellido": "Silva",
  "documento": "55555555",
  "cargo": "Senior Developer",
  "departamento": "IT",
  "email": "roberto.silva@empresa.com",
  "telefono": "555-0006",
  "fechaIngreso": "2024-01-15T00:00:00",
  "salario": 55000.00,
  "activo": true
}
```

## Modelo de Datos

### Personal
- **Id**: Identificador único (auto-incremento)
- **Nombre**: Nombre del empleado (requerido, máx. 100 caracteres)
- **Apellido**: Apellido del empleado (requerido, máx. 100 caracteres)
- **Documento**: Número de documento (único, máx. 20 caracteres)
- **Cargo**: Puesto de trabajo (máx. 50 caracteres)
- **Departamento**: Departamento al que pertenece (máx. 50 caracteres)
- **Email**: Correo electrónico (único, máx. 100 caracteres)
- **Telefono**: Número de teléfono (máx. 20 caracteres)
- **FechaIngreso**: Fecha de ingreso a la empresa
- **Salario**: Salario (decimal 10,2)
- **Activo**: Estado activo/inactivo (por defecto: true)
- **FechaCreacion**: Fecha de creación del registro (automático)
- **FechaActualizacion**: Fecha de última actualización

## Comandos Útiles

```bash
# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run

# Crear migración (si usas Code First)
dotnet ef migrations add InitialCreate

# Actualizar base de datos
dotnet ef database update
```

## Arquitectura

La API sigue una arquitectura en capas:

1. **Controladores**: Manejan las peticiones HTTP
2. **Servicios**: Contienen la lógica de negocio
3. **Modelos**: Definen las entidades de datos
4. **Data**: Contexto de Entity Framework

## Logging

La aplicación incluye logging automático para:
- Errores en operaciones de base de datos
- Excepciones no controladas
- Información de depuración

## Seguridad

- ✅ **Autenticación JWT**: Todos los endpoints están protegidos con tokens JWT
- ✅ Validación de modelos automática
- ✅ Manejo seguro de excepciones
- ✅ Índices únicos en campos críticos

### Autenticación JWT

La API ahora incluye autenticación JWT completa:

- **Login**: `POST /api/auth/login`
- **Verificación**: `GET /api/auth/verify`
- **Usuarios de prueba**: admin/admin123, usuario/usuario123, personal/personal123

Para más detalles, consulta: [JWT_AUTH_GUIDE.md](./JWT_AUTH_GUIDE.md)

## Próximas Mejoras

- [x] Autenticación JWT
- [ ] Paginación en listados
- [ ] Filtros avanzados
- [ ] Caching
- [ ] Tests unitarios
- [ ] Docker containerization

## Autor

Desarrollado como parte del proyecto API Personal SIMAI.
