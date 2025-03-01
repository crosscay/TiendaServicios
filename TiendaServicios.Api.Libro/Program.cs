using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Implement;

var builder = WebApplication.CreateBuilder(args);
// Obtener el objeto Configuration
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

//builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddDbContext<ContextoLibreria>(options => {
    options.UseSqlServer(configuration.GetConnectionString("ConexionDb")); // Asegúrate de ajustar el nombre de la conexión según tu configuración
});

// builder.Services.AddTransient<IRabbitEventBus, RabbitEventBus>();

builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(sp.GetRequiredService<IMediator>(), scopeFactory);
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
