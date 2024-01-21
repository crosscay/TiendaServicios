using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using static TiendaServicios.Api.Autor.Aplicacion.Consulta;
using TiendaServicios.Api.Autor.Persistencia;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            public readonly ContextoAutor _contexto;
            public readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibro.Where(x=>x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();

                if (autor == null) 
                {
                    throw new Exception("No se encontro el autor");
                }

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

                return autorDto;
            }
        }
    }
}
