// Importamos todas las librerias que necesitamos para el controlador
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
    // Este controlador maneja todas las operaciones de productos
    //Se define la ruta primero - todas las rutas van a empezar con /api/productos
    // IMPORTANTE: Este controlador es ASINCRONO - puede manejar miles de peticiones simultaneas
    // Cada metodo async libera el hilo del servidor mientras espera operaciones de BD
    [Route("api/productos")]
    [ApiController]
    public class ProductosController:ControllerBase
    {
        // Variables privadas que necesitamos para que funcione el controlador
        private readonly IOutputCacheStore outputCacheStore; // Para manejar el cache de forma asincrona
        private readonly ApplicationDbContext context; // Para conectarnos a la base de datos de forma asincrona
        private readonly IMapper mapper; // Para convertir entre DTOs y entidades (sincrono)
        private const string cacheTag = "productos"; // Etiqueta para el cache
        // Constructor que recibe las dependencias que necesita el controlador
        // ASP.NET Core las inyecta automaticamente usando inyeccion de dependencias
        // IMPORTANTE: Las dependencias estan configuradas para ser thread-safe y asincronas
        public ProductosController(IOutputCacheStore outputCacheStore,ApplicationDbContext context, IMapper mapper)
        {
            this.outputCacheStore = outputCacheStore;
            this.context = context;
            this.mapper = mapper;
        }
        // METODO GET - Obtener todos los productos con paginacion
        //crear una accion
        // Estamos indicando que esta accion que vamos a crear responde a el metodo get
        // [HttpGet("ObtenerTodos")] // api/generos/ObtenerTodos
        [HttpGet]
        [OutputCache(Tags = [cacheTag])] // Le decimos que cachee esta respuesta
        // ASINCRONO: async Task significa que este metodo no bloquea el hilo principal
        // Mientras espera la respuesta de la base de datos, puede atender otras peticiones
        // VENTAJAS DE LA ASINCRONIA:
        // 1. El servidor puede manejar miles de peticiones simultaneas
        // 2. No se bloquea esperando operaciones lentas (BD, APIs externas)
        // 3. Mejor rendimiento y escalabilidad
        public async Task<List<ProductoDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            // obteniendo la queryable de productos - esto es como hacer SELECT * FROM Productos
            var queryable = context.Productos;
            // insertando los parametros de paginacion en la cabecera http
            // Esto calcula cuantos registros hay en total y lo pone en la cabecera
            // ASINCRONO: await hace que espere a que termine esta operacion antes de continuar
            // PERO: mientras espera, el hilo se libera para atender otras peticiones
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            // Retornamos los productos ordenados por nombre, paginados y convertidos a DTO
            // ASINCRONO: ToListAsync() ejecuta la consulta en la base de datos de forma asincrona
            // Mientras espera, el servidor puede atender otras peticiones
            // FLUJO ASINCRONO:
            // 1. Se inicia la consulta a la BD
            // 2. El hilo se libera y puede atender otras peticiones
            // 3. Cuando la BD responde, se reanuda la ejecucion
            // 4. Se retorna el resultado
            return await queryable
                .OrderBy(p=>p.Name) // Ordenamos por nombre
                .Paginar(paginacion) // Aplicamos la paginacion
                .ProjectTo<ProductoDTO>(mapper.ConfigurationProvider).ToListAsync(); // Convertimos a DTO

        }

        // METODO GET - Obtener un producto por su ID
        [HttpGet("{id}", Name = "ObtenerProductoPorId"),]
        [OutputCache] // Cacheamos esta respuesta tambien
        // ASINCRONO: async Task<ActionResult> permite que el metodo no bloquee el hilo
        // Esto es crucial para el rendimiento del servidor
        // EJEMPLO DE ASINCRONIA:
        // - Peticion 1: Busca producto ID 1 (tarda 100ms)
        // - Peticion 2: Busca producto ID 2 (llega mientras la 1 espera)
        // - Peticion 3: Busca producto ID 3 (llega mientras las otras esperan)
        // - Todas se procesan en paralelo, no una despues de otra
        async public Task<ActionResult<ProductoDTO>> Get(int id)
        {
            // Buscamos el producto por ID y lo convertimos a DTO
            // ASINCRONO: FirstOrDefaultAsync() hace la consulta a la base de datos sin bloquear
            // El await hace que espere la respuesta pero libera el hilo para otras tareas
            var producto = await context.Productos
                .ProjectTo<ProductoDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);
            // Si no lo encontramos, devolvemos 404
            if (producto is null)
            {
                return NotFound();
            }
            // Si lo encontramos, lo devolvemos
            return producto;
        }
        // METODO POST - Crear un nuevo producto
        [HttpPost]
        // ASINCRONO: async Task permite manejar operaciones de base de datos sin bloquear
        // Esto es especialmente importante para operaciones de escritura que pueden tardar
        // OPERACIONES DE ESCRITURA SON MAS LENTAS:
        // - Validacion de datos
        // - Insercion en BD
        // - Actualizacion de indices
        // - Invalidacion de cache
        // La asincronia permite que otras peticiones no esperen
        public async Task<IActionResult> Post([FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            // Convertimos el DTO a entidad usando AutoMapper
            var producto = mapper.Map<Producto>(productoCreacionDTO);
            //var producto = new Producto { Name = productoCreacionDTO.Name, Category = productoCreacionDTO.Category, Unite = productoCreacionDTO.Unite };
            // Esto es como una instruccion sql insert - agregamos el producto al contexto
            context.Add(producto);
            // ASINCRONO: SaveChangesAsync() guarda en la base de datos de forma asincrona
            // Mientras espera la confirmacion de la BD, el servidor puede atender otras peticiones
            // DETALLES DE LA ASINCRONIA:
            // 1. Se inicia la transaccion en la BD
            // 2. El hilo se libera y puede procesar otras peticiones
            // 3. Cuando la BD confirma, se reanuda la ejecucion
            // 4. Se continua con el siguiente await
            await context.SaveChangesAsync();
            // ASINCRONO: EvictByTagAsync() invalida el cache de forma asincrona
            // Esto asegura que la lista se actualice sin bloquear el hilo
            // IMPORTANTE: Este await tambien libera el hilo mientras busca en cache
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            // Devolvemos 201 Created con la ruta del nuevo producto
            return CreatedAtRoute("ObtenerProductoPorId", new { id = producto.Id }, producto);

        }
        // METODO PUT - Actualizar un producto existente
        [HttpPut("{id:int}")]
        // ASINCRONO: async Task permite manejar operaciones de BD sin bloquear el servidor
        // Esto es crucial para el rendimiento cuando hay muchas peticiones simultaneas
        // ACTUALIZACIONES PUEDEN SER COMPLEJAS:
        // - Verificar que existe
        // - Validar datos
        // - Actualizar en BD
        // - Actualizar cache
        // - Manejar transacciones
        // La asincronia mantiene el servidor responsivo
        public async Task<IActionResult> Put(int id, [FromBody] ProductoCreacionDTO productoCreacionDTO)
        {
            // Verificamos si el producto existe
            // ASINCRONO: AnyAsync() hace la consulta a la BD sin bloquear el hilo
            // Esta consulta es rapida pero aun asi se hace de forma asincrona
            var productoExiste = await context.Productos.AnyAsync(p => p.Id == id);
                if (!productoExiste)
            {
                return NotFound(); // Si no existe, devolvemos 404
            }
            // Convertimos el DTO a entidad y le asignamos el ID
            var producto = mapper.Map<Producto>(productoCreacionDTO);
            producto.Id = id;
            // Marcamos el producto como modificado
            context.Update(producto);
            // ASINCRONO: Invalidamos el cache de forma asincrona
            // Mientras busca y elimina del cache, el hilo se libera
            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            // ASINCRONO: Guardamos los cambios en la BD de forma asincrona
            // Mientras espera, el servidor puede procesar otras peticiones
            // ACTUALIZACION ASINCRONA:
            // 1. Se inicia la transaccion UPDATE
            // 2. El hilo se libera para otras peticiones
            // 3. Cuando la BD confirma, se reanuda
            // 4. Se retorna la respuesta
            await context.SaveChangesAsync();
            // Devolvemos 204 No Content (actualizacion exitosa)
            return NoContent();
            ;
        }
        // METODO DELETE - Eliminar un producto
        [HttpDelete("{id:int}")]
        // ASINCRONO: async Task permite eliminar sin bloquear el servidor
        // Esto es importante porque las operaciones de eliminacion pueden tardar
        // ELIMINACIONES PUEDEN SER COSTOSAS:
        // - Verificar integridad referencial
        // - Eliminar de BD
        // - Actualizar indices
        // - Invalidar cache
        // - Logs de auditoria
        // La asincronia permite que otras operaciones continúen
        public async Task<IActionResult> Delete(int id)
        {
            // Buscamos el producto que queremos eliminar
            // ASINCRONO: FirstOrDefaultAsync() consulta la BD sin bloquear
            // Esta consulta se ejecuta de forma asincrona para no bloquear el servidor
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            // Si no existe, devolvemos 404
            if (producto is null)
            {
                return NotFound();
            }

            // Marcamos el producto para eliminacion
            context.Remove(producto);
            // ASINCRONO: SaveChangesAsync() elimina de la BD de forma asincrona
            // Mientras espera la confirmacion, el servidor puede atender otras peticiones
            // ELIMINACION ASINCRONA:
            // 1. Se inicia la transaccion DELETE
            // 2. El hilo se libera para procesar otras peticiones
            // 3. Cuando la BD confirma la eliminacion, se reanuda
            // 4. Se continua con la invalidacion del cache
            await context.SaveChangesAsync();
            // ASINCRONO: EvictByTagAsync() invalida el cache sin bloquear
            // Mientras busca y elimina del cache, el hilo se libera
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            // Devolvemos 204 No Content (eliminacion exitosa)
            return NoContent();
        }


    }
}
