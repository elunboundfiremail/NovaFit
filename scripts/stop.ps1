#!/usr/bin/env pwsh
# Script de parada - NovaFit Backend

Write-Host "🛑 Deteniendo NovaFit..." -ForegroundColor Yellow

Set-Location "$PSScriptRoot\..\devops"
docker-compose down

Write-Host "✅ Servicios detenidos" -ForegroundColor Green
