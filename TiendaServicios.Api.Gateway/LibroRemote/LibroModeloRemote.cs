using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Gateway.LibroRemote
{
    public class LibroModeloRemote
    {
        public Guid? LibreriaMaterialId { get; set; }

        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public Guid? AutorLibro { get; set; }

        public AutorModeloRemote AutorData { get; set; }

    }
}
