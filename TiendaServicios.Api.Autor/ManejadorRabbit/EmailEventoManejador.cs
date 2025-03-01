using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Modelo;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;
using Config = Microsoft.Extensions.Configuration.IConfiguration;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;
        private readonly ISendGridEnviar _sendGridEnviar;
        private readonly Config _configuration;

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, ISendGridEnviar sendGridEnviar, Config configuration)
        {
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }

        public EmailEventoManejador() { }

        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation(@event.Titulo);

            var configApiKey = _configuration["Sengrid:ApiKey"];

            var objData = new SendGridData
            {
                SendGridAPIKey = configApiKey,
                EmailDestinatario = @event.Destinatario,
                NombreDestinatario = @event.Destinatario,
                Titulo = @event.Titulo,
                Contenido = @event.Contenido
            };

            var resultado = await _sendGridEnviar.EnviarEmail(objData);

            if (resultado.resultado)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}
