using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Persistencia;
using Microsoft.Extensions.Configuration;
using TiendaServicios.Api.Autor.Aplicacion;
using MediatR;
using FluentValidation.AspNetCore;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Implement;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;
using TiendaServicios.Api.Autor.ManejadorRabbit;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Implement;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración al builder
builder.Configuration.AddJsonFile("appsettings.json");

// Obtener el objeto Configuration
var configuration = builder.Configuration;


builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

// Add services to the container.
builder.Services.AddDbContext<ContextoAutor>(options => {
    options.UseNpgsql(configuration.GetConnectionString("ConexionDatabase")); // Asegúrate de ajustar el nombre de la conexión según tu configuración
});

builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(sp.GetRequiredService<IMediator>(), scopeFactory);
});

builder.Services.AddSingleton<ISendGridEnviar, SendGridEnviar>();

builder.Services.AddTransient<EmailEventoManejador>();

builder.Services.AddTransient<IEventoManejador<EmailEventoQueue>, EmailEventoManejador>();

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddAutoMapper(typeof(Consulta.Manejador));

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

// Integración de RabbitMQ y suscripción a eventos
using (var scope = app.Services.CreateScope())
{
    var eventBus = scope.ServiceProvider.GetRequiredService<IRabbitEventBus>();
    eventBus.Subscribe<EmailEventoQueue, EmailEventoManejador>();
}

app.Run();
