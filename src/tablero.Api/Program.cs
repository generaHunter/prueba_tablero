

using tablero.Api;
using tablero.Application;
using tablero.Application.DataBase;
using tablero.Application.DataBase.Reportes.ReporteTareas;
using tablero.Application.DataBase.Tablero.Commands.CreateTablero;
using tablero.Application.DataBase.Tablero.DefaultModel;
using tablero.Application.DataBase.Tarea.Commands.CreateTarea;
using tablero.Application.DataBase.Tarea.Commands.DefultModel;
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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapPost("/createEstado", async (IDataBaseService _dataBaseService, IMongoDataBaseService _mongoDataBaseServic) =>
{
    var entyty = new tablero.Domain.Entities.Estado.EstadoEntity
    {
        NombreEstado = "Completada 2"
    };

    await _dataBaseService.Estado.AddAsync(entyty);
    bool result = await _dataBaseService.SaveAsync();

    if (result) await _mongoDataBaseServic.Estado.InsertOneAsync(entyty);

    return "Create Ok";
});

app.MapPost("/createUsuario", async (IReporteTareasQuery service, ICreateTableroCommand serviceTablero, ICreateTareaCommand serviceTarea) =>
{
    //var model = new UpdateUsuarioModel { 
    //UserId = 2,
    //FirstName = "Diana Mercedes",
    //LastName = "Diaz",
    //Password = "admin01",
    //UserName = "dd"
    //};

    var modelTablero = new DefaultTableroModel()
    {
        Descripcion = "Tablero prueba",
        FechaCreacion = DateTime.UtcNow,
        Nombre = "Tablero 1",
        UserId = 1
    };

    var modelTarea = new DefaultTareaModel()
    {
        UserId = 1,
        Descripcion = "Tarea de prueba",
        Titulo = "Tarea 1",
        IdTablero = 2
    };



    //return await serviceTablero.Execute(modelTablero);
    //return await serviceTarea.Execute(modelTarea);
    return await service.Execute();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

