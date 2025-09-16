using Microsoft.EntityFrameworkCore;

namespace backend.utilidades
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            // esto contara la cantidad de registros que hay en la tabla productos
            double cantidad = await queryable.CountAsync();
            // Ahora se agrega la cabecera personalizada
            httpContext.Response.Headers.Append("cantidadTotalRegistros", cantidad.ToString());

        }
    }
}
