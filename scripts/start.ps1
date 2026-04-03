#!/usr/bin/env pwsh
# Script de inicio rápido - NovaFit Backend

Write-Host "🚀 NovaFit - Inicio Rápido" -ForegroundColor Cyan
Write-Host ""

# 1. Verificar Docker
Write-Host "1️⃣ Verificando Docker..." -ForegroundColor Yellow
docker ps > $null 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Docker no está corriendo. Inicia Docker Desktop primero." -ForegroundColor Red
    exit 1
}
Write-Host "✅ Docker OK" -ForegroundColor Green

# 2. Levantar servicios
Write-Host ""
Write-Host "2️⃣ Levantando servicios Docker..." -ForegroundColor Yellow
Set-Location "$PSScriptRoot\..\devops"
docker-compose up -d
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Error al levantar servicios" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Servicios corriendo" -ForegroundColor Green

# 3. Esperar a que PostgreSQL esté listo
Write-Host ""
Write-Host "3️⃣ Esperando PostgreSQL (15s)..." -ForegroundColor Yellow
Start-Sleep -Seconds 15
Write-Host "✅ PostgreSQL listo" -ForegroundColor Green

# 4. Aplicar migraciones
Write-Host ""
Write-Host "4️⃣ Aplicando migraciones..." -ForegroundColor Yellow
Set-Location "$PSScriptRoot\..\backend"
dotnet ef database update --project src\NovaFit.Infrastructure --startup-project src\NovaFit.WebAPI
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Error aplicando migraciones" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Base de datos actualizada + Seed cargado" -ForegroundColor Green

# 5. Ejecutar API
Write-Host ""
Write-Host "5️⃣ Iniciando API..." -ForegroundColor Yellow
Write-Host ""
Write-Host "🎉 Sistema listo:" -ForegroundColor Green
Write-Host "  📊 API:       http://localhost:5000" -ForegroundColor Cyan
Write-Host "  🔐 Keycloak:  http://localhost:8080 (admin/Upds123)" -ForegroundColor Cyan
Write-Host "  🗄️  PostgreSQL: localhost:5432 (novafit_user/Upds123)" -ForegroundColor Cyan
Write-Host ""

dotnet run --project src\NovaFit.WebAPI --urls "http://localhost:5000"
