using AutoMapper;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
