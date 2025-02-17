using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }

        public DateTime? FechaCreacionSesion { get; set; }

        public List<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
