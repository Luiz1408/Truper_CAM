# Script para descargar e instalar PostgreSQL
Write-Host "Descargando PostgreSQL..."
$url = "https://get.enterprisedb.com/postgresql/postgresql-13.16-1-windows-x64.exe"
$output = "$env:TEMP\postgresql-installer.exe"

# Descargar el instalador
Invoke-WebRequest -Uri $url -OutFile $output

Write-Host "Iniciando instalación de PostgreSQL..."
Write-Host "Por favor, siga las instrucciones del instalador:"
Write-Host "- Puerto: 5432 (por defecto)"
Write-Host "- Contraseña del usuario postgres: Fernando"
Write-Host "- Marque la opción para instalar pgAdmin"

# Iniciar el instalador
Start-Process -FilePath $output -Wait

Write-Host "Instalación completada. Por favor, reinicie su terminal y ejecute:"
Write-Host "1. Start-Service postgres*"
Write-Host "2. psql -U postgres -c 'CREATE DATABASE trupercam;'"
Write-Host "3. dotnet ef database update"
Write-Host "4. dotnet run"
