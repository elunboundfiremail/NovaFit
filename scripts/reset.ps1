#!/usr/bin/env pwsh
# Script de reset completo - NovaFit Backend

Write-Host "🔄 Reset Completo de NovaFit" -ForegroundColor Cyan
Write-Host ""

# 1. Detener servicios
Write-Host "1️⃣ Deteniendo servicios..." -ForegroundColor Yellow
Set-Location "$PSScriptRoot\..\devops"
docker-compose down -v
Write-Host "✅ Servicios detenidos y volúmenes eliminados" -ForegroundColor Green

# 2. Reiniciar todo
Write-Host ""
Write-Host "2️⃣ Reiniciando sistema..." -ForegroundColor Yellow
& "$PSScriptRoot\start.ps1"
