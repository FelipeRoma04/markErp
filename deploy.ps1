# ============================================================================
# FASE 4 - Task 40: MarkERP Automated Deployment Script
# PowerShell script for unattended/guided deployment
# 
# Usage:
#   .\deploy.ps1                    # Interactive setup wizard
#   .\deploy.ps1 -SqlServer "."    # Specify custom SQL Server
#   .\deploy.ps1 -SkipDb            # Install app only, skip DB setup
# ============================================================================

param(
    [string]$SqlServer = ".",
    [switch]$SkipDb = $false,
    [string]$DatabaseName = "dbProyecto",
    [string]$InstallPath = "C:\Program Files\MarkERP"
)

Clear-Host

Write-Host "======================================" -ForegroundColor Cyan
Write-Host "MarkERP Deployment Script" -ForegroundColor Cyan
Write-Host "Version 1.0" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""

# Check for Admin privileges
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")
if (-not $isAdmin) {
    Write-Host "ERROR: Administrator privileges required!" -ForegroundColor Red
    Write-Host "Please run PowerShell as Administrator and try again." -ForegroundColor Yellow
    exit 1
}

# Function: Check SQL Server connectivity
function Test-SqlServer {
    param([string]$ServerName)
    
    try {
        $conn = New-Object System.Data.SqlClient.SqlConnection
        $conn.ConnectionString = "Server=$ServerName;Database=master;Integrated Security=true;Connection Timeout=5"
        $conn.Open()
        $conn.Close()
        return $true
    }
    catch {
        return $false
    }
}

# Function: Execute SQL script
function Invoke-SqlScript {
    param(
        [string]$ServerName,
        [string]$ScriptPath,
        [string]$LogPath
    )
    
    Write-Host "Executing: $(Split-Path $ScriptPath -Leaf)" -ForegroundColor Yellow
    
    try {
        $cmd = "sqlcmd.exe -S $ServerName -E -i `"$ScriptPath`" -o `"$LogPath`" -b"
        Invoke-Expression $cmd | Out-Null
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✓ Script executed successfully" -ForegroundColor Green
            return $true
        }
        else {
            Write-Host "✗ Script execution failed (Error Code: $LASTEXITCODE)" -ForegroundColor Red
            Write-Host "Log: $LogPath" -ForegroundColor Yellow
            return $false
        }
    }
    catch {
        Write-Host "✗ ERROR: $_" -ForegroundColor Red
        return $false
    }
}

# Main Deployment Flow
Write-Host ""
Write-Host "Deployment Configuration:" -ForegroundColor Cyan
Write-Host "  SQL Server: $SqlServer"
Write-Host "  Database: $DatabaseName"
Write-Host "  Install Path: $InstallPath"
Write-Host "  Skip Database Setup: $SkipDb"
Write-Host ""

# Validate prerequisites
Write-Host "Validating Prerequisites..." -ForegroundColor Cyan

if (-not (Test-Path $InstallPath)) {
    Write-Host "✓ Installation path accessible" -ForegroundColor Green
}
else {
    $response = Read-Host "Installation path exists. Overwrite? (Y/N)"
    if ($response -ne 'Y') {
        Write-Host "Deployment cancelled." -ForegroundColor Yellow
        exit 0
    }
}

if (-not $SkipDb) {
    Write-Host "Testing SQL Server connection..." -ForegroundColor Yellow
    if (Test-SqlServer -ServerName $SqlServer) {
        Write-Host "✓ SQL Server is accessible" -ForegroundColor Green
    }
    else {
        Write-Host "✗ Cannot connect to SQL Server at '$SqlServer'" -ForegroundColor Red
        Write-Host "Ensure SQL Server is running and accessible." -ForegroundColor Yellow
        exit 1
    }
}

# Create installation directory
Write-Host ""
Write-Host "Creating installation structure..." -ForegroundColor Cyan
New-Item -ItemType Directory -Path $InstallPath -Force | Out-Null
New-Item -ItemType Directory -Path "$InstallPath\Database" -Force | Out-Null
New-Item -ItemType Directory -Path "$InstallPath\Backups" -Force | Out-Null
Write-Host "✓ Installation directory created" -ForegroundColor Green

# Copy application files (from installer output)
Write-Host ""
Write-Host "Copying application files..." -ForegroundColor Cyan
$sourceDir = Split-Path -Parent $PSScriptRoot
$binDir = "$sourceDir\bin\Debug"

if (Test-Path $binDir) {
    Copy-Item "$binDir\*.exe" "$InstallPath\" -Force
    Copy-Item "$binDir\*.dll" "$InstallPath\" -Force 2>$null
    Copy-Item "$binDir\*.config" "$InstallPath\" -Force 2>$null
    Write-Host "✓ Application files copied" -ForegroundColor Green
}
else {
    Write-Host "✗ Application files not found at $binDir" -ForegroundColor Red
    exit 1
}

# Copy database scripts
Write-Host ""
Write-Host "Copying database scripts..." -ForegroundColor Cyan
$dbDir = "$sourceDir\Database"
if (Test-Path $dbDir) {
    Get-ChildItem "$dbDir\*.sql" | ForEach-Object {
        Copy-Item $_.FullName "$InstallPath\Database\" -Force
    }
    Write-Host "✓ Database scripts copied" -ForegroundColor Green
}

# Setup database
if (-not $SkipDb) {
    Write-Host ""
    Write-Host "Setting up database..." -ForegroundColor Cyan
    
    $masterScript = "$InstallPath\Database\10_Master_Installation.sql"
    if (Test-Path $masterScript) {
        $logFile = "$InstallPath\db_setup.log"
        Invoke-SqlScript -ServerName $SqlServer -ScriptPath $masterScript -LogPath $logFile
    }
    else {
        Write-Host "✗ Master installation script not found" -ForegroundColor Red
        Write-Host "Manual database setup may be required." -ForegroundColor Yellow
    }
}

# Create shortcuts
Write-Host ""
Write-Host "Creating shortcuts..." -ForegroundColor Cyan

$exePath = "$InstallPath\Proyecto.exe"
if (Test-Path $exePath) {
    $desktopPath = [Environment]::GetFolderPath("Desktop")
    $link = "$desktopPath\MarkERP.lnk"
    
    $WshShell = New-Object -ComObject WScript.Shell
    $shortCut = $WshShell.CreateShortcut($link)
    $shortCut.TargetPath = $exePath
    $shortCut.WorkingDirectory = $InstallPath
    $shortCut.Description = "MarkERP Enterprise Resource Planning System"
    $shortCut.Save()
    
    Write-Host "✓ Desktop shortcut created" -ForegroundColor Green
}

# Create Start Menu entry
$startMenuPath = [Environment]::GetFolderPath("CommonStartMenu")
$appFolder = "$startMenuPath\Programs\MarkERP"
New-Item -ItemType Directory -Path $appFolder -Force | Out-Null

$link = "$appFolder\MarkERP.lnk"
$WshShell = New-Object -ComObject WScript.Shell
$shortCut = $WshShell.CreateShortcut($link)
$shortCut.TargetPath = $exePath
$shortCut.WorkingDirectory = $InstallPath
$shortCut.Save()

Write-Host "✓ Start Menu entry created" -ForegroundColor Green

# Firewall configuration
Write-Host ""
$fwResponse = Read-Host "Add application to Windows Firewall? (Y/N)"
if ($fwResponse -eq 'Y') {
    try {
        New-NetFirewallRule -DisplayName "MarkERP" -Direction Inbound -Program $exePath -Action Allow -ErrorAction SilentlyContinue | Out-Null
        Write-Host "✓ Firewall rule added" -ForegroundColor Green
    }
    catch {
        Write-Host "⚠ Could not add firewall rule" -ForegroundColor Yellow
    }
}

# Final Report
Write-Host ""
Write-Host "======================================" -ForegroundColor Green
Write-Host "Deployment Completed Successfully!" -ForegroundColor Green
Write-Host "======================================" -ForegroundColor Green
Write-Host ""
Write-Host "Installation Details:" -ForegroundColor Cyan
Write-Host "  Install Location: $InstallPath"
Write-Host "  Executable: $exePath"
Write-Host "  Database: $DatabaseName"
Write-Host "  SQL Server: $SqlServer"
Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Cyan
Write-Host "1. Launch MarkERP from Desktop or Start Menu"
Write-Host "2. Login with: admin / admin123"
Write-Host "3. Change default password immediately (Settings ⚙️)"
Write-Host "4. Configure company information (Configuración)"
Write-Host "5. Start using the system!"
Write-Host ""
Write-Host "Support Files:" -ForegroundColor Cyan
Write-Host "  Database Scripts: $InstallPath\Database\"
Write-Host "  Backups Location: $InstallPath\Backups\"
Write-Host "  Debug Log: $InstallPath\debug.log"
Write-Host "  Backup Log: $InstallPath\backup_log.txt"
Write-Host ""
Write-Host "Enjoy your MarkERP system!" -ForegroundColor Green
Write-Host ""

# Offer to launch app
$launchResponse = Read-Host "Launch MarkERP now? (Y/N)"
if ($launchResponse -eq 'Y') {
    &$exePath
}
