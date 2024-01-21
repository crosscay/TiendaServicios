using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Persistencia;
using Microsoft.Extensions.Configuration;
using TiendaServicios.Api.Autor.Aplicacion;
using MediatR;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuraci�n al builder
builder.Configuration.AddJsonFile("appsettings.json");

// Obtener el objeto Configuration
var configuration = builder.Configuration;


builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

// Add services to the container.
builder.Services.AddDbContext<ContextoAutor>(options => {
    options.UseNpgsql(configuration.GetConnectionString("ConexionDatabase")); // Aseg�rate de ajustar el nombre de la conexi�n seg�n tu configuraci�n
});

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

app.Run();
