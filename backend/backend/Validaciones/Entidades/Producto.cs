using pelicualasAPI.validaciones;
using System.ComponentModel.DataAnnotations;

namespace pelicualasAPI.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        [PrimeraLetraMayusculaAtribute]
        [Required(ErrorMessage ="El campo {0} es Obligatorio Chaval")]
        [StringLength(10, ErrorMessage ="El campo debe de tener {1} caracateres")]
        public required string Name { get; set; }
        public int Category { get; set; }
        public int Unite { get; set; }

    }
    public class ProductoSend
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Category { get; set; }
        public int Unite { get; set; }

    }
}
