using backend.DTOs;

namespace backend.utilidades
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable,PaginacionDTO paginacion)
        {
            // Esto lo que hace es saltarse los registros que no se van a mostrar, y luego toma los que se van a mostrar
            return queryable.
                // Skip la cantidad de registros que se van a saltar
                Skip((paginacion.Pagina - 1) * paginacion.RecordsPorPagina)
                // Take la cantidad de registros que se van a mostrar
                .Take(paginacion.RecordsPorPagina);

        }
    }
}
