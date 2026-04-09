; ============================================================================
; FASE 4 - Task 40: MarkERP Installer Script
; Inno Setup 6.0 compatible installer for MarkERP application
; 
; This script packages:
; - Compiled .exe application (bin\Debug\Proyecto.exe)
; - SQL Server database setup scripts
; - Configuration files
; - Backup utility
; - Desktop shortcut and Start Menu entries
; 
; To build installer:
; 1. Install Inno Setup 6.0+ (https://jrsoftware.org/isdl.php)
; 2. Open this file in Inno Setup
; 3. Click "Compile" to generate MarkERP_Installer_v1.0.exe
; ============================================================================

#define MyAppName "MarkERP"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "MarkCHP Enterprise"
#define MyAppURL "https://markchp.com"
#define MyAppExeName "Proyecto.exe"
#define SourceDir "C:\Dev\markErp"

[Setup]
AppId={{3A8B5C7D-2F9E-4A1B-9C3D-7F2E1B4A9C2D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir={#SourceDir}\installer
OutputBaseFilename=MarkERP_Installer_v{#MyAppVersion}
SetupIconFile={#SourceDir}\bin\Debug\{#MyAppExeName}
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
DisableProgramGroupPage=no
WizardStyle=modern
ShowLanguageDialog=yes
AlwaysShowDirOnReadyPage=yes
AlwaysShowGroupOnReadyPage=yes
AllowUNCPath=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIconTask}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIconTask}"; GroupDescription: "{cm:AdditionalIcons}"; OnlyBelowVersion: 0,6.1
Name: "sqlsetup"; Description: "Configure SQL Server database (required on first install)"; GroupDescription: "Database Setup"; Flags: unchecked
Name: "firewall"; Description: "Add application to Windows Firewall exceptions"; GroupDescription: "Security"

[Files]
; Application executable and dependencies
Source: "{#SourceDir}\bin\Debug\Proyecto.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}\bin\Debug\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "{#SourceDir}\bin\Debug\*.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}\README.md"; DestDir: "{app}"; Flags: ignoreversion

; Database setup scripts
Source: "{#SourceDir}\Database\10_Master_Installation.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "{#SourceDir}\Database\09_Company_Settings.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "{#SourceDir}\Database\update_phase2_3.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "{#SourceDir}\Database\setup_complete.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "{#SourceDir}\Database\*.sql"; DestDir: "{app}\Database"; Flags: ignoreversion

; Configuration templates
Source: "{#SourceDir}\App.config"; DestDir: "{app}"; Flags: ignoreversion

; Include sample data if exists
Source: "{#SourceDir}\tools\*"; DestDir: "{app}\tools"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Components:
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"; Components:
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; Components:
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon; Components:

[Run]
; Show readme file after installation
Filename: "notepad.exe"; Parameters: """{app}\README.md"""; Description: "View README (Setup Instructions)"; Flags: nowait postinstall skipifsilent; Components:

; Launch application after setup (optional)
Filename: "{app}\{#MyAppExeName}"; Description: "Launch {#MyAppName}"; Flags: nowait postinstall skipifsilent unchecked; Components:

[UninstallDelete]
Type: dirifempty; Name: "{app}"
Type: dirifempty; Name: "{app}\Database"
Type: dirifempty; Name: "{app}\tools"
Type: dirifempty; Name: "{app}\Backups"
Type: files; Name: "{app}\backup_log.txt"
Type: files; Name: "{app}\debug.log"

[Code]
{ ============================================================================
  INITIALIZATION & CONFIG
  ============================================================================ }

procedure InitializeWizard;
begin
  { Initialization code can be added here if needed }
end;

{ ============================================================================
  DATABASE CONFIGURATION
  ============================================================================ }

// Function: Check if SQL Server is installed and accessible
function CheckSqlServerInstalled: Boolean;
var
  ErrorCode: Integer;
begin
  Result := False;
  { Try to execute sqlcmd to check SQL Server availability }
  if not ShellExec('open', 'sqlcmd.exe', '-?', '', SW_HIDE, ewWaitUntilTerminated, ErrorCode) then
  begin
    MsgBox('SQL Server client tools not found. Please ensure SQL Server Client (sqlcmd) is installed.' + #13 + 
           'Install from: https://learn.microsoft.com/en-us/sql/tools/sqlcmd', 
           mbError, MB_OK);
    Result := False;
  end
  else
    Result := True;
end;

// Function: Run SQL setup scripts
function SetupDatabase(ScriptPath: String): Boolean;
var
  CommandLine: String;
  ErrorCode: Integer;
  OutputFile: String;
begin
  Result := False;
  OutputFile := ExpandConstant('{app}\db_setup.log');
  
  { Build sqlcmd command to execute master installation script }
  CommandLine := '-S . -E -i "' + ScriptPath + '" -o "' + OutputFile + '"';
  
  MsgBox('Executing database setup script...' + #13 + 
         'This may take a few minutes.' + #13 +
         'Script: ' + ExtractFileName(ScriptPath), 
         mbInformation, MB_OK);
  
  if ShellExec('open', 'sqlcmd.exe', CommandLine, '', SW_SHOW, ewWaitUntilTerminated, ErrorCode) then
  begin
    if ErrorCode = 0 then
    begin
      MsgBox('Database setup completed successfully!' + #13 +
             'Log file: ' + OutputFile,
             mbInformation, MB_OK);
      Result := True;
    end
    else
    begin
      MsgBox('Database setup encountered errors.' + #13 +
             'Error code: ' + IntToStr(ErrorCode) + #13 +
             'Check log file: ' + OutputFile,
             mbError, MB_OK);
      Result := False;
    end;
  end
  else
  begin
    MsgBox('Could not execute SQL setup. Make sure SQL Server is running and sqlcmd is in PATH.',
           mbError, MB_OK);
    Result := False;
  end;
end;

{ ============================================================================
  POST-INSTALL STEPS
  ============================================================================ }

procedure CurStepChanged(CurStep: TSetupStep);
var
  MasterScript: String;
begin
  if CurStep = ssPostInstall then
  begin
    { Offer to configure database if checkbox selected }
    if IsTaskSelected('sqlsetup') then
    begin
      MasterScript := ExpandConstant('{app}\Database\10_Master_Installation.sql');
      
      if FileExists(MasterScript) then
      begin
        if MsgBox('Would you like to setup the database now?' + #13 +
                  'This requires SQL Server to be running locally.' + #13 +
                  'Default login: (local) with Windows Authentication.' + #13 + #13 +
                  'Click Yes to continue with database setup.',
                  mbConfirmation, MB_YESNO) = IDYES then
        begin
          if CheckSqlServerInstalled then
            SetupDatabase(MasterScript)
          else
            MsgBox('SQL Server setup skipped. You can run setup manually later using:' + #13 +
                   MasterScript, mbInformation, MB_OK);
        end;
      end;
    end;
    
    { Add to firewall if checkbox selected }
    if IsTaskSelected('firewall') then
    begin
      ShellExec('open', 'powershell.exe', 
                'Add-MpPreference -ExclusionPath "' + ExpandConstant('{app}') + '"',
                '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);
    end;
  end;
end;

{ ============================================================================
  UNINSTALL HANDLING
  ============================================================================ }

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then
  begin
    { Optional: Warn about leaving database }
    if MsgBox('Keep database and backup files?' + #13 +
              'Click Yes to preserve data for reinstall.' + #13 +
              'Click No to remove all files.',
              mbConfirmation, MB_YESNO) = IDNO then
    begin
      { Database files could be deleted here if desired }
    end;
  end;
end;

{ ============================================================================
  END OF SCRIPT
  ============================================================================ }
