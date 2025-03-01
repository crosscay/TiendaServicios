using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Gateway.LibroRemote
{
    public class AutorModeloRemote
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string AutorLibroGuid { get; set; }


    }
}
