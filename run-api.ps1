$env:DOTNET_CLI_HOME = 'C:\Dev\markErp'
Set-Location 'C:\Dev\markErp'
dotnet run --project Proyecto.Api/Proyecto.Api.csproj --configuration Debug --no-build --urls http://localhost:5000 *> C:\Dev\markErp\api-run.log
