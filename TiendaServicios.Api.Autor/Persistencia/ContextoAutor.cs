using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Persistencia
{
    public class ContextoAutor: DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options) { }

        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configuración específica para la entidad AutorLibro
        //    modelBuilder.Entity<AutorLibro>().Property(x => x.FechaNacimiento)
        //        .HasColumnType("timestamp with time zone"); // Configurar el tipo de columna en PostgreSQL

        //    // Puedes agregar otras configuraciones aquí si es necesario...

        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
