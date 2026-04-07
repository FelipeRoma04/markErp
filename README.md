# markErp

Aplicación ERP ligera (WinForms + API .NET 8) para gestión de departamentos, empleados, asistencia, contratos, ventas, inventario y contabilidad básica.  

## Componentes
- **Proyecto.exe**: Cliente de escritorio WinForms (.NET Framework 4.7.2).
- **Proyecto.Api**: API REST en .NET 8 (endpoints CRUD y reportes).
- **Database/**: Scripts SQL de creación de tablas y datos iniciales.

## Ejecución rápida
1) API: `dotnet run --project Proyecto.Api/Proyecto.Api.csproj --configuration Debug --urls http://localhost:5000`
2) Cliente: `bin\Debug\Proyecto.exe` (tras compilar `Proyecto.csproj`).
