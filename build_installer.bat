@echo off
REM ============================================================================
REM FASE 4 - Task 40: MarkERP Installer Build Script
REM 
REM This batch file:
REM 1. Compiles the project in Release mode
REM 2. Creates installer directory structure
REM 3. Copies required files for installer
REM 4. Provides instructions for Inno Setup compilation
REM ============================================================================

setlocal enabledelayedexpansion

echo.
echo ========================================
echo MarkERP Installer Build Script
echo ========================================
echo.

REM Check if we're in the correct directory
if not exist "Proyecto.csproj" (
    echo ERROR: Proyecto.csproj not found. Please run this script from the project root directory.
    pause
    exit /b 1
)

REM Create installer directories
echo Creating installer directories...
if not exist "installer" mkdir installer
if not exist "installer\Database" mkdir installer\Database
if not exist "installer\tools" mkdir installer\tools

REM Build in Release mode
echo.
echo Building project in Release configuration...
call dotnet publish -c Release -r win-x64 --self-contained -o "installer\output"

if errorlevel 1 (
    echo ERROR: Build failed!
    pause
    exit /b 1
)

echo Build completed successfully.

REM Copy database scripts to installer
echo.
echo Copying database setup scripts...
copy Database\10_Master_Installation.sql installer\Database\ >nul 2>&1
copy Database\09_Company_Settings.sql installer\Database\ >nul 2>&1
copy Database\update_phase2_3.sql installer\Database\ >nul 2>&1
copy Database\setup_complete.sql installer\Database\ >nul 2>&1
copy Database\*.sql installer\Database\ >nul 2>&1

REM Copy configuration files
echo Copying configuration files...
copy App.config installer\output\ >nul 2>&1
copy README.md installer\ >nul 2>&1

REM Copy tools
echo Copying tools...
copy tools\backup-db.ps1 installer\tools\ >nul 2>&1

echo.
echo ========================================
echo Installer Package Preparation Complete
echo ========================================
echo.
echo Next Steps:
echo 1. Install Inno Setup 6.0 (https://jrsoftware.org/isdl.php)
echo 2. Open the Inno Setup IDE
echo 3. Open: installer\MarkERP_Installer.iss
echo 4. Click "Compile" to generate the .exe installer
echo    (Output will be: installer\MarkERP_Installer_v1.0.exe)
echo.
echo Installer Contents:
echo - Compiled application (.exe + dependencies)
echo - SQL database setup scripts
echo - Database configuration wizard
echo - Desktop shortcut creation option
echo - Start Menu integration
echo.
echo Installation Requirements:
echo - Windows This file compiles application:bin\Debug\Proyecto.exe for release build
echo - SQL Server 2019 or later (LocalDB or Express recommended for SMBs)
echo - .NET Runtime (included in self-contained build)
echo.
echo Default Paths After Installation:
echo - Application: C:\Program Files\MarkERP\Proyecto.exe
echo - Backups: C:\Program Files\MarkERP\Backups\
echo - Database Scripts: C:\Program Files\MarkERP\Database\
echo.
pause
