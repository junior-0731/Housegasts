// Importamos las librerias necesarias para Entity Framework
using Microsoft.EntityFrameworkCore;
using pelicualasAPI.Entidades;

namespace backend
{
    // Esta clase es la que se conecta con la base de datos
    // Hereda de DbContext que es la clase base de Entity Framework
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuracion de la base de datos
        // Estas opciones vienen del Program.cs donde configuramos la conexion
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        
        // Aqui definimos las tablas que vamos a tener en la base de datos
        // Cada DbSet representa una tabla
        // para configurar la clase producto como una entidad es asi
        // Es decir, esto ya representaria una tabla en nuestra base de datos
        public DbSet<Producto> Productos { get; set; }

    }
}
