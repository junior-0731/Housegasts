// Importamos las librerias que necesitamos para que funcione el backend
using backend;
using backend.utilidades;
using Microsoft.EntityFrameworkCore;

// Creamos el builder de la aplicacion, esto es como el constructor principal
var builder = WebApplication.CreateBuilder(args);

// Aqui es donde configuramos todos los servicios que va a usar la aplicacion

// Agregamos los controladores, esto es lo que maneja las rutas de la API
builder.Services.AddControllers();
// Configuramos Swagger para que podamos ver y probar la API desde el navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Lo agregamos dos veces porque a veces da problemas si no, es redundante pero funciona
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agregamos AutoMapper para convertir entre DTOs y entidades automaticamente
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Configuramos la base de datos, le decimos que use SQL Server
// IMPORTANTE: Entity Framework maneja la asincronia automaticamente
// Todas las operaciones de BD se ejecutan de forma asincrona por defecto
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    // Le decimos que use la cadena de conexion que esta en appsettings.json
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
    // Esta linea comentada es otra forma de hacerlo pero la de arriba es mejor
    // opciones.UseSqlServer("name=DefaultConnection");
});
// Configuramos el cache para que las respuestas se guarden por 15 segundos
// ASINCRONO: El cache se maneja de forma asincrona para no bloquear las peticiones
builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(15);

});

// Configuramos CORS para que Angular pueda hacer peticiones al backend
builder.Services.AddCors(options =>
{
    // Creamos una politica llamada "AllowAngularDev"
    options.AddPolicy("AllowAngularDev",
        policy => policy
            // Solo permitimos peticiones desde localhost:4200 (donde corre Angular)
            .WithOrigins("http://localhost:4200")
            // Exponemos la cabecera cantidadTotalRegistros para la paginacion
            .WithExposedHeaders("cantidadTotalRegistros")
            // Permitimos cualquier tipo de cabecera en las peticiones
            .AllowAnyHeader()
            // Permitimos cualquier metodo HTTP (GET, POST, PUT, DELETE)
            .AllowAnyMethod());
});
   



// Construimos la aplicacion con todas las configuraciones que hicimos arriba
// IMPORTANTE: La aplicacion ASP.NET Core maneja las peticiones de forma asincrona
// Esto significa que puede atender miles de peticiones simultaneas sin bloquearse
var app = builder.Build();

// Aplicamos la politica de CORS que configuramos arriba
app.UseCors("AllowAngularDev");

// Configuramos el pipeline de HTTP, esto es el orden en que se ejecutan las cosas

// Solo en desarrollo mostramos Swagger para probar la API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirigimos HTTP a HTTPS por seguridad
app.UseHttpsRedirection();

// Activamos el cache que configuramos arriba
app.UseOutputCache();


// Activamos la autorizacion (aunque no la estamos usando aun)
app.UseAuthorization();

// Mapeamos los controladores para que las rutas funcionen
app.MapControllers();

// Ejecutamos la aplicacion y la dejamos corriendo
// ASINCRONO: app.Run() maneja todas las peticiones HTTP de forma asincrona
// Cada peticion se procesa en su propio hilo sin bloquear las demas
app.Run();
