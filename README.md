# Housegasts
Gestor Inteligente de Compras y Despensa Personal

### Lenguajes
    * C# .NET Core 8.0 
    * Angular
    * Sql Server

## Archivos y carpetas ignorados
Este repositorio ignora archivos de dependencias, builds, configuraciones locales y temporales, como:
- `node_modules/`, `bin/`, `obj/`, `dist/`, `coverage/`
- `.vscode/`, `.idea/`, archivos de logs y temporales
- Archivos de configuración local como `appsettings.Development.json`, `.env`

Quien clone el repo debe crear sus propios archivos de configuración local. Se recomienda usar archivos de ejemplo si están disponibles.

## Configuración y ejecución

### Frontend (Angular)
```powershell
cd front
npm install
npm start # o ng serve
```

### Backend (ASP.NET Core)
```powershell
cd ../backend/backend
 dotnet restore
 dotnet run
```

Asegúrate de tener instalado:
- Node.js
- Angular CLI
- .NET 6/8 SDK


appsetting.json --> backend
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=COMPUTADOR1066;Database=house_gasts;Integrated Security=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
