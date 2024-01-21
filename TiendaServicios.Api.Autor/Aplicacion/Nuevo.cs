using FluentValidation;
using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }

        }

        public class EjecutaValidacion: AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion() 
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contexto) 
            {
                _contexto = contexto;
            }
            //public string Nombre { get; set; }
            //public string Apellido { get; set; }
            //public DateTime? FechaNacimiento { get; set; }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                if (request.FechaNacimiento.HasValue)
                {
                    request.FechaNacimiento = request.FechaNacimiento.Value.ToUniversalTime();
                }
                // DateTime miDateTime = DateTime.Now; // o cualquier otra instancia de DateTime que tengas
                // DateTime miDateTimeUtc = miDateTime.ToUniversalTime();
                var autoLibro = new AutorLibro()
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellido = request.Apellido,
                    AutorLibroGuid = Guid.NewGuid().ToString(),

                };
                _contexto.AutorLibro.Add(autoLibro);
                var valor = await _contexto.SaveChangesAsync();
                //throw new NotImplementedException();

                if (valor > 0) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Autor del libro");
            }
        }
    }
}
