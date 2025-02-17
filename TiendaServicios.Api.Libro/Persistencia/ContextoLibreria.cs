//using Microsoft.EntityFrameworkCore;
//using TiendaServicios.Api.Libro.Modelo;

//namespace TiendaServicios.Api.Libro.Persistencia
//{
//    public class ContextoLibreria: DbContext
//    {
//        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }

//        public DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
//    }
//}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria() { }

        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }

    }
}
