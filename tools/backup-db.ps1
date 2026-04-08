$ErrorActionPreference = "Stop"

# Configuración: ajusta instancia y ruta de backups
$Instance = "localhost\SQLEXPRESS"
$Database = "dbProyecto"
$BackupDir = "C:\Dev\markErp\backups"

if (-not (Test-Path $BackupDir)) {
    New-Item -ItemType Directory -Path $BackupDir | Out-Null
}

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$backupPath = Join-Path $BackupDir "$Database-$timestamp.bak"

Write-Host "Respaldando $Database en $backupPath ..."

sqlcmd -S $Instance -Q "BACKUP DATABASE [$Database] TO DISK='$backupPath' WITH INIT, COMPRESSION"

Write-Host "Backup terminado."
