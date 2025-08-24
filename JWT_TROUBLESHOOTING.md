# 🔧 Solución de Problemas JWT - Token Inválido

## 🐛 Problema: "invalid_token" Error

Si recibes el error `www-authenticate: Bearer error="invalid_token"`, sigue estos pasos:

### ✅ Paso 1: Verificar que la API esté funcionando

1. Ve a: `http://localhost:5054/swagger`
2. Prueba el endpoint: `GET /api/test/public` (no requiere autenticación)
3. Debe retornar: `{"message": "API funcionando correctamente", ...}`

### ✅ Paso 2: Obtener un Token Válido

1. En Swagger, ve a: `POST /api/auth/login`
2. Usa estas credenciales:
```json
{
  "username": "admin",
  "password": "admin123"
}
```
3. **IMPORTANTE**: Copia el token COMPLETO de la respuesta

### ✅ Paso 3: Autorizar en Swagger

1. Haz clic en el botón **"Authorize"** (🔒) en la parte superior
2. En el campo, introduce EXACTAMENTE:
```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.tu_token_completo_aqui...
```
3. **IMPORTANTE**: 
   - Debe empezar con `Bearer ` (con espacio)
   - El token debe estar completo (sin cortar)
   - No agregar comillas

### ✅ Paso 4: Probar Endpoint Protegido

1. Prueba primero: `GET /api/test/protected`
2. Debe retornar información del usuario autenticado
3. Si funciona, prueba: `GET /api/personal`

### 🔍 Debugging con cURL

Si quieres probar fuera de Swagger:

```bash
# 1. Obtener token
curl -X POST "http://localhost:5054/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'

# 2. Usar token (reemplaza TOKEN_AQUI con tu token real)
curl -X GET "http://localhost:5054/api/test/protected" \
  -H "Authorization: Bearer TOKEN_AQUI"
```

### 🔍 Debugging con PowerShell

```powershell
# 1. Login
$loginData = @{username="admin"; password="admin123"} | ConvertTo-Json
$response = Invoke-RestMethod -Uri "http://localhost:5054/api/auth/login" -Method Post -Body $loginData -ContentType "application/json"
$token = $response.token

# 2. Usar token
$headers = @{"Authorization" = "Bearer $token"}
Invoke-RestMethod -Uri "http://localhost:5054/api/test/protected" -Headers $headers
```

### ❌ Errores Comunes

1. **Token cortado**: Asegúrate de copiar el token COMPLETO
2. **Falta "Bearer "**: El header debe ser `Bearer {token}`, no solo `{token}`
3. **Token expirado**: Los tokens duran 60 minutos, haz login nuevamente
4. **Espacios extra**: No agregues espacios adicionales
5. **Comillas**: No pongas el token entre comillas

### 🔧 Si Aún No Funciona

1. **Revisa la consola** donde corre la API para ver errores
2. **Usa el endpoint de debug**: `GET /api/test/headers` para ver qué headers estás enviando
3. **Verifica la configuración** en `appsettings.json`

### 📋 Configuración Actual

La configuración JWT actual es:
- **Issuer**: `PersonalAPI`
- **Audience**: `PersonalAPI_Users`
- **Expiración**: 60 minutos
- **SecretKey**: Configurada en appsettings.json

### 🎯 Prueba Rápida

Para probar rápidamente:

1. Abre: `http://localhost:5054/swagger`
2. Login con `admin/admin123`
3. Copia el token completo
4. Autorizar con `Bearer {token}`
5. Probar `GET /api/test/protected`

¡Si sigues estos pasos exactamente, la autenticación JWT debe funcionar! 🚀
