namespace backend.DTOs
{
    // DTO para manejar la paginacion de los resultados
    // Se usa para dividir los resultados en paginas y no cargar todo de una vez
    public class PaginacionDTO
    {
        // Por defecto a pagina sera 1 - numero de pagina actual
        public int Pagina { get; set; } = 1;
        
        // Cantidad de registros por pagina (privado para controlarlo)
        private int recordsPorPagina = 10;
        
        // Limite maximo de registros por pagina para evitar sobrecarga
        private readonly int cantidadMaximaRecordsPorPagina = 20;
        
        // Propiedad publica para acceder a recordsPorPagina con validacion
        public int RecordsPorPagina { get
            {
                return recordsPorPagina;
            } set
            {
                // Esto limita la cantidad maxima de records por pagina, para  que no coloque mas de la permitida que es 20
                // Si intentan poner mas de 20, se queda en 20
                recordsPorPagina = (value > cantidadMaximaRecordsPorPagina) ? cantidadMaximaRecordsPorPagina : value;
            }}
    }
}
