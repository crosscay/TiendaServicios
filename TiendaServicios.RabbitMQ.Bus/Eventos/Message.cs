using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TiendaServicios.RabbitMQ.Bus.Eventos
{
    public abstract class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }

        protected Message() {
            MessageType = GetType().Name;
        }

    }
}
