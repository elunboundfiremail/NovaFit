#!/usr/bin/env pwsh
# Script para obtener token JWT de Keycloak

Write-Host "🔐 Obteniendo Token JWT de Keycloak" -ForegroundColor Cyan
Write-Host ""

$keycloakUrl = "http://localhost:8080/realms/novafit-realm/protocol/openid-connect/token"
$body = @{
    grant_type = "password"
    client_id = "novafit-api"
    username = "admin_novafit"
    password = "Upds123"
}

try {
    Write-Host "Solicitando token..." -ForegroundColor Yellow
    $response = Invoke-RestMethod -Uri $keycloakUrl -Method Post -Body $body -ContentType "application/x-www-form-urlencoded"
    
    Write-Host ""
    Write-Host "✅ Token obtenido exitosamente" -ForegroundColor Green
    Write-Host ""
    Write-Host "Access Token:" -ForegroundColor Yellow
    Write-Host $response.access_token -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Expira en: $($response.expires_in) segundos" -ForegroundColor Yellow
    
    # Guardar en variable de entorno
    $env:NOVAFIT_JWT = $response.access_token
    Write-Host ""
    Write-Host "✅ Token guardado en variable: `$env:NOVAFIT_JWT" -ForegroundColor Green
}
catch {
    Write-Host "❌ Error obteniendo token: $($_.Exception.Message)" -ForegroundColor Red
}
