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
        public List<ProductoSend> Get()
        {
            // Obtiene los productos de la base de datos y los mapea a ProductoSend
            return context.Productos
                .Select(p => new ProductoSend
                {
                    Id = p.Id,
                    Name = p.Name,
                    // aca debe de ir una relacion para obtener la categoria de la tabla de categorias
                    Category = p.Category.ToString(), // Si Category ya es string, elimina el ToString()
                    Unite = p.Unite
                })
                .ToList();
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
