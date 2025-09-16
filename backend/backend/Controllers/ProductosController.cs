using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.DTOs;
using backend.utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;
        private const string cacheTag = "productos";
        public ProductosController(IOutputCacheStore outputCacheStore,ApplicationDbContext context, IMapper mapper)
        {
            this.outputCacheStore = outputCacheStore;
            this.context = context;
            this.mapper = mapper;
        }
        //crear una accion
        // Estamos indicando que esta accion que vamos a crear responde a el metodo get
        // [HttpGet("ObtenerTodos")] // api/generos/ObtenerTodos
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<ProductoDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            // obteniendo la queryable de productos
            var queryable = context.Productos;
            // insertando los parametros de paginacion en la cabecera http
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            return await queryable
                .OrderBy(p=>p.Name)
                .Paginar(paginacion)
                .ProjectTo<ProductoDTO>(mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("{id}", Name = "ObtenerProductoPorId"),]
        [OutputCache]
        async public Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await context.Productos
                .ProjectTo<ProductoDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (producto is null)
            {
                return NotFound();
            }
            return producto;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = mapper.Map<Producto>(productoCreacionDTO);
            //var producto = new Producto { Name = productoCreacionDTO.Name, Category = productoCreacionDTO.Category, Unite = productoCreacionDTO.Unite };
            // Esto es como una instruccion sql insert
            context.Add(producto);
            // Y esto es como un commit
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return CreatedAtRoute("ObtenerProductoPorId", new { id = producto.Id }, producto);

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            var productoExiste = await context.Productos.AnyAsync(p => p.Id == id);
                if (!productoExiste)
            {
                return NotFound();
            }
            var producto = mapper.Map<Producto>(productoCreacionDTO);
            producto.Id = id;
            context.Update(producto);
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            await context.SaveChangesAsync();
            return NoContent();
            ;
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            if (producto is null)
            {
                return NotFound();
            }

            context.Remove(producto);
            await context.SaveChangesAsync();

            return NoContent();
        }


    }
}
