using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.BusRabbit
{
    public interface IEventoManejador<in TEvent> : IEventoManejador where TEvent: Evento
    {
        Task Handle(TEvent @event);
    }

    public interface IEventoManejador { 
    
    }
    
}
