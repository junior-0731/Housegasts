using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using pelicualasAPI.Entidades;

namespace backend.Controllers
{
    //Se define la ruta primero
    [Route("api/productos")]
    [ApiController]
    public class ProductosController:ControllerBase
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext context;  
        private const string cacheTag = "productos";
        public ProductosController(IOutputCacheStore outputCacheStore,ApplicationDbContext context)
        {
            this.outputCacheStore = outputCacheStore;
            this.context = context;
        }
        //crear una accion
        // Estamos indicando que esta accion que vamos a crear responde a el metodo get
        // [HttpGet("ObtenerTodos")] // api/generos/ObtenerTodos
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public List<Producto> Get()
        {
            return new List<Producto>()
                {
                    new Producto { Id = 1, Name = "Bolsa Yogures", Category = 0, Unite = 1 },
                    new Producto { Id = 2, Name = "Leche Entera", Category = 0, Unite = 1 },
                    new Producto { Id = 3, Name = "Pan Integral", Category = 0, Unite = 1 },
                    new Producto { Id = 4, Name = "Huevos (12 unidades)", Category = 0, Unite = 1 },
                    new Producto { Id = 5, Name = "Manzana Roja", Category = 0, Unite = 1 },
                    new Producto { Id = 6, Name = "Zanahoria", Category = 0, Unite = 1 },
                    new Producto { Id = 7, Name = "Pollo Entero", Category = 0, Unite = 1 },
                    new Producto { Id = 8, Name = "Arroz Blanco", Category = 0, Unite = 1 },
                    new Producto { Id = 9, Name = "Aceite de Oliva", Category = 0, Unite = 1 },
                    new Producto { Id = 10, Name = "Queso Rallado", Category = 0, Unite = 1 },
                    new Producto { Id = 11, Name = "Café Molido", Category = 0, Unite = 1 },
                    new Producto { Id = 12, Name = "Pasta Espagueti", Category = 0, Unite = 1 },
                    new Producto { Id = 13, Name = "Tomate", Category = 0, Unite = 1 },
                    new Producto { Id = 14, Name = "Atún en Lata", Category = 0, Unite = 1 },
                    new Producto { Id = 15, Name = "Galletas Integrales", Category = 0, Unite = 1 }
                };

                 }


        [HttpGet("{id}", Name = "ObtenerProductoPorId"),]
        [OutputCache]
        async public Task<ActionResult<Producto>> Get(int id)
        {
            throw new NotImplementedException();


        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto )
        {
            // Esto es como una instruccion sql insert
            context.Add(producto);
            // Y esto es como un commit
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerProductoPorId", new { id = producto.Id }, producto);

        }

    }
}
