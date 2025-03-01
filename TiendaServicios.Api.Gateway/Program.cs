using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using TiendaServicios.Api.Gateway.ImplementRemote;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.MessageHandler;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuración de Ocelot desde el archivo JSON
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddHttpClient("AutorService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autor"]);
});

// builder.Services.AddOcelot();
builder.Services.AddSingleton<IAutorRemote, AutorRemote>();
builder.Services.AddOcelot().AddDelegatingHandler<LibroHandler>();
// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app =  builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
