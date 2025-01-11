

using tablero.Api;
using tablero.Application.DataBase;
using tablero.Persistence;
using tablero.Common;
using tablero.Application;
using tablero.External;
using tablero.Persistence.DataBase;
using tablero.Application.DataBase.Estado.Commands.CreateEstado;
using tablero.Application.DataBase.Usuario.Commands.CreateUsuario;
using tablero.Application.DataBase.Usuario.DefaultModel;
using tablero.Application.DataBase.Usuario.Commands.UpdateUsuario;
using tablero.Application.DataBase.Usuario.Commands.DeleteUsuario;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddWebApi()
    .AddCommon()
    .AddApplication()
    .AddExternal(builder.Configuration)
    .AddPersitence(builder.Configuration);

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

app.MapPost("/createUsuario", async (IDeleteUsuarioCommand service) =>
{
    //var model = new UpdateUsuarioModel { 
    //UserId = 2,
    //FirstName = "Diana Mercedes",
    //LastName = "Diaz",
    //Password = "admin01",
    //UserName = "dd"
    //};

   return await service.Execute(2);
});

app.Run();

