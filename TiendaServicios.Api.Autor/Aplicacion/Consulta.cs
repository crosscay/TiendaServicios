using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using static TiendaServicios.Api.Autor.Aplicacion.Nuevo;
using TiendaServicios.Api.Autor.Persistencia;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            public readonly ContextoAutor _contexto;
            public readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibro.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);

                return autoresDto;
            }
        }
    }
}
