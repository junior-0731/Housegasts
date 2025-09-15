using Microsoft.EntityFrameworkCore;
using pelicualasAPI.Entidades;

namespace backend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        // para configurar la clase producto como una entidad es asi
        // Es decir, esto ya representaria una tabla en nuestra base de datos
        public DbSet<Producto> Productos { get; set; }

    }
}
