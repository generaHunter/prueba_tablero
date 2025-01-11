

using tablero.Application.DataBase;
using tablero.Persistence;
using tablero.Persistence.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersitence(builder.Configuration);

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

app.Run();

