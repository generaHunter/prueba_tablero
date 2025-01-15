

using tablero.Api;
using tablero.Application;
using tablero.Common;
using tablero.External;
using tablero.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddWebApi()
    .AddCommon()
    .AddApplication()
    .AddExternal(builder.Configuration)
    .AddPersitence(builder.Configuration);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173")  // El origen permitido (frontend)
               .AllowAnyMethod()                     // Permitir cualquier método HTTP
               .AllowAnyHeader()                     // Permitir cualquier cabecera
               .AllowCredentials();                  // Permitir cookies y credenciales si es necesario
    });
});

builder.Services.AddControllers();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();

