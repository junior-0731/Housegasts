namespace backend.DTOs
{
    public class PaginacionDTO
    {
        // Por defecto a pagina sera 1
        public int Pagina { get; set; } = 1;
        private int recordsPorPagina = 10;
        private readonly int cantidadMaximaRecordsPorPagina = 20;
        public int RecordsPorPagina { get
            {
                return recordsPorPagina;
            } set
            {
                // Esto limita la cantidad maxima de records por pagina, para  que no coloque mas de la permitida que es 50
                recordsPorPagina = (value > cantidadMaximaRecordsPorPagina) ? cantidadMaximaRecordsPorPagina : value;
            }}
    }
}
