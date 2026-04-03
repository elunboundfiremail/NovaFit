#!/usr/bin/env pwsh
# Script para liberar puertos - Ejecutar como Administrador
#Requires -RunAsAdministrator

Write-Host "🛑 Liberando puertos para NovaFit" -ForegroundColor Cyan
Write-Host ""

# Puerto 5432 (PostgreSQL)
Write-Host "1️⃣ Liberando puerto 5432 (PostgreSQL)..." -ForegroundColor Yellow
$proc5432 = netstat -ano | findstr ":5432.*LISTENING" | ForEach-Object { ($_ -split '\s+')[-1] } | Select-Object -First 1
if ($proc5432) {
    Write-Host "   Deteniendo PID: $proc5432" -ForegroundColor Yellow
    Stop-Process -Id $proc5432 -Force -ErrorAction SilentlyContinue
    Write-Host "✅ Puerto 5432 liberado" -ForegroundColor Green
} else {
    Write-Host "✅ Puerto 5432 ya está libre" -ForegroundColor Green
}

Write-Host ""

# Puerto 8080 (Keycloak)
Write-Host "2️⃣ Liberando puerto 8080 (Keycloak)..." -ForegroundColor Yellow
$proc8080 = netstat -ano | findstr ":8080.*LISTENING" | ForEach-Object { ($_ -split '\s+')[-1] } | Select-Object -First 1
if ($proc8080) {
    Write-Host "   Deteniendo PID: $proc8080" -ForegroundColor Yellow
    Stop-Process -Id $proc8080 -Force -ErrorAction SilentlyContinue
    Write-Host "✅ Puerto 8080 liberado" -ForegroundColor Green
} else {
    Write-Host "✅ Puerto 8080 ya está libre" -ForegroundColor Green
}

Write-Host ""

# Puerto 5000 (API)
Write-Host "3️⃣ Liberando puerto 5000 (API)..." -ForegroundColor Yellow
$proc5000 = netstat -ano | findstr ":5000.*LISTENING" | ForEach-Object { ($_ -split '\s+')[-1] } | Select-Object -First 1
if ($proc5000) {
    Write-Host "   Deteniendo PID: $proc5000" -ForegroundColor Yellow
    Stop-Process -Id $proc5000 -Force -ErrorAction SilentlyContinue
    Write-Host "✅ Puerto 5000 liberado" -ForegroundColor Green
} else {
    Write-Host "✅ Puerto 5000 ya está libre" -ForegroundColor Green
}

Write-Host ""
Write-Host "🎉 Puertos liberados. Ahora puedes ejecutar start.ps1" -ForegroundColor Green
