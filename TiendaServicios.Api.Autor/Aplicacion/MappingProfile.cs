using AutoMapper;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
