using backend;
using backend.utilidades;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
    // opciones.UseSqlServer("name=DefaultConnection");
});
builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(15);

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .WithExposedHeaders("cantidadTotalRegistros")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
   



var app = builder.Build();

app.UseCors("AllowAngularDev");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOutputCache();


app.UseAuthorization();

app.MapControllers();

app.Run();
