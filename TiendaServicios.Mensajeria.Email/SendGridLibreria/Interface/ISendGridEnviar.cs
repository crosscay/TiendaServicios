using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Modelo;

namespace TiendaServicios.Mensajeria.Email.SendGridLibreria.Interface
{
    public interface ISendGridEnviar
    {
        Task<(bool resultado, string errorMessage)> EnviarEmail(SendGridData data);
    }
}
