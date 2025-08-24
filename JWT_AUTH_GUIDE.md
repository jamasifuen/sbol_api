# Guía de Autenticación JWT - Personal API

## 🔐 Autenticación JWT Implementada

La API Personal SIMAI ahora incluye autenticación JWT (JSON Web Token) para proteger todos los endpoints de Personal.

## 📋 Configuración JWT

### Configuración en appsettings.json

```json
{
  "JwtSettings": {
    "SecretKey": "PersonalAPI_SuperSecretKey_MinimumLengthRequired_ForJWT_Signature_2024",
    "Issuer": "PersonalAPI",
    "Audience": "PersonalAPI_Users",
    "ExpiryInMinutes": 60
  }
}
```

## 🚀 Endpoints de Autenticación

### 1. Login (Obtener Token)

**POST** `/api/auth/login`

```json
{
  "username": "admin",
  "password": "admin123"
}
```

**Respuesta exitosa:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "expires": "2024-12-21T10:30:00Z",
  "success": true,
  "message": "Login exitoso"
}
```

### 2. Verificar Token

**GET** `/api/auth/verify`
- Requiere: `Authorization: Bearer {token}`

### 3. Usuarios de Ejemplo

**GET** `/api/auth/users`
- Muestra usuarios disponibles para pruebas

## 👥 Usuarios de Prueba

| Usuario | Contraseña | Rol |
|---------|------------|-----|
| admin | admin123 | Administrador |
| usuario | usuario123 | Usuario |
| personal | personal123 | Personal |

## 🔧 Cómo Usar la Autenticación

### 1. Obtener Token

```bash
curl -X POST "http://localhost:5054/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "admin123"
  }'
```

### 2. Usar Token en Requests

```bash
curl -X GET "http://localhost:5054/api/personal" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

### 3. En Swagger UI

1. Ir a: `http://localhost:5054/swagger`
2. Hacer clic en **"Authorize"** (🔒)
3. Introducir: `Bearer {tu_token}`
4. Ahora puedes usar todos los endpoints protegidos

### 4. Con JavaScript/Fetch

```javascript
// 1. Login
const loginResponse = await fetch('/api/auth/login', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    username: 'admin',
    password: 'admin123'
  })
});

const authData = await loginResponse.json();
const token = authData.token;

// 2. Usar token en requests
const personalResponse = await fetch('/api/personal', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
});
```

## 🛡️ Endpoints Protegidos

Todos los endpoints de Personal ahora requieren autenticación:

- `GET /api/personal` - Obtener todo el personal
- `GET /api/personal/{codigo}` - Obtener por código
- `POST /api/personal` - Crear personal
- `PUT /api/personal/{codigo}` - Actualizar personal
- `DELETE /api/personal/{codigo}` - Eliminar personal
- `GET /api/personal/departamento/{dept}` - Por departamento
- `GET /api/personal/activos` - Solo activos

## ⚠️ Importante para Producción

1. **Cambiar SecretKey**: Usar una clave más segura y compleja
2. **Variables de Entorno**: No hardcodear la clave en appsettings.json
3. **Base de Datos de Usuarios**: Implementar tabla de usuarios con passwords hasheados
4. **HTTPS**: Usar siempre HTTPS en producción
5. **Refresh Tokens**: Implementar refresh tokens para sesiones largas

## 🔄 Flujo de Autenticación

```mermaid
sequenceDiagram
    participant Client
    participant AuthAPI
    participant PersonalAPI
    
    Client->>AuthAPI: POST /api/auth/login
    AuthAPI->>Client: JWT Token
    Client->>PersonalAPI: GET /api/personal + Bearer Token
    PersonalAPI->>Client: Data Response
```

## 🧪 Pruebas con cURL

```bash
# 1. Login
TOKEN=$(curl -s -X POST "http://localhost:5054/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}' | \
  jq -r '.token')

# 2. Usar token
curl -X GET "http://localhost:5054/api/personal" \
  -H "Authorization: Bearer $TOKEN"

# 3. Verificar token
curl -X GET "http://localhost:5054/api/auth/verify" \
  -H "Authorization: Bearer $TOKEN"
```

## 🐛 Solución de Problemas

### Error 401 Unauthorized
- Verificar que el token esté incluido en el header
- Verificar que el token no haya expirado
- Verificar formato: `Authorization: Bearer {token}`

### Token Inválido
- Verificar que la SecretKey sea la correcta
- Verificar que el token no esté corrupto

### Token Expirado
- Hacer login nuevamente para obtener un nuevo token
- Verificar la configuración de `ExpiryInMinutes`

¡La autenticación JWT está completamente implementada y lista para usar! 🎉
