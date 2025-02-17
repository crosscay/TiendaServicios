using MediatR;
using TiendaServicios.Api.Libro.Modelo;
using static TiendaServicios.Api.Libro.Aplicacion.Nuevo;
using TiendaServicios.Api.Libro.Persistencia;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDto>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {
            public readonly ContextoLibreria _contexto;
            public readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libros);

                return librosDto;
            }
        }

        //public class ListaAutor : IRequest<List<LibroMaterialDto>>
        //{

        //}

        //public class Manejador : IRequestHandler<ListaAutor, List<LibroMaterialDto>>
        //{
        //    public readonly ContextoLibreria _contexto;
        //    public readonly IMapper _mapper;

        //    public Manejador(ContextoLibreria contexto, IMapper mapper)
        //    {
        //        _contexto = contexto;
        //        _mapper = mapper;
        //    }

        //    public async Task<List<LibroMaterialDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
        //    {
        //        var autores = await _contexto.AutorLibro.ToListAsync();
        //        var autoresDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(autores);

        //        return autoresDto;
        //    }
        //}
    }
}
