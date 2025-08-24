# Script de prueba JWT para Personal API
# Ejecutar desde PowerShell

$baseUrl = "http://localhost:5054"

Write-Host "=== Prueba de API Personal SIMAI - JWT ===" -ForegroundColor Green
Write-Host ""

# 1. Probar endpoint público
Write-Host "1. Probando endpoint público..." -ForegroundColor Yellow
try {
    $publicResponse = Invoke-RestMethod -Uri "$baseUrl/api/test/public" -Method Get
    Write-Host "✅ Endpoint público funciona:" -ForegroundColor Green
    $publicResponse | ConvertTo-Json
} catch {
    Write-Host "❌ Error en endpoint público: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""

# 2. Hacer login para obtener token
Write-Host "2. Haciendo login..." -ForegroundColor Yellow
$loginData = @{
    username = "admin"
    password = "admin123"
} | ConvertTo-Json

try {
    $loginResponse = Invoke-RestMethod -Uri "$baseUrl/api/auth/login" -Method Post -Body $loginData -ContentType "application/json"
    Write-Host "✅ Login exitoso:" -ForegroundColor Green
    $loginResponse | ConvertTo-Json
    $token = $loginResponse.token
    Write-Host "Token obtenido: $($token.Substring(0, 50))..." -ForegroundColor Cyan
} catch {
    Write-Host "❌ Error en login: $($_.Exception.Message)" -ForegroundColor Red
    exit
}

Write-Host ""

# 3. Probar endpoint protegido con token
Write-Host "3. Probando endpoint protegido..." -ForegroundColor Yellow
$headers = @{
    "Authorization" = "Bearer $token"
}

try {
    $protectedResponse = Invoke-RestMethod -Uri "$baseUrl/api/test/protected" -Method Get -Headers $headers
    Write-Host "✅ Endpoint protegido funciona:" -ForegroundColor Green
    $protectedResponse | ConvertTo-Json
} catch {
    Write-Host "❌ Error en endpoint protegido: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Detalles del error:" -ForegroundColor Red
    $_.Exception | Out-String
}

Write-Host ""

# 4. Probar endpoint de Personal
Write-Host "4. Probando endpoint de Personal..." -ForegroundColor Yellow
try {
    $personalResponse = Invoke-RestMethod -Uri "$baseUrl/api/personal" -Method Get -Headers $headers
    Write-Host "✅ Endpoint Personal funciona:" -ForegroundColor Green
    Write-Host "Cantidad de registros: $($personalResponse.Count)" -ForegroundColor Cyan
} catch {
    Write-Host "❌ Error en endpoint Personal: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Detalles del error:" -ForegroundColor Red
    $_.Exception | Out-String
}

Write-Host ""
Write-Host "=== Fin de las pruebas ===" -ForegroundColor Green
