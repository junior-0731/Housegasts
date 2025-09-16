using pelicualasAPI.validaciones;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class ProductoCreacionDTO
    {
            [PrimeraLetraMayusculaAtribute]
            [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "El campo {0} es Obligatorio Chaval")]
            [StringLength(10, ErrorMessage = "El campo debe de tener {1} caracateres")]
            public required string Name { get; set; }
            public int Category { get; set; }
            public int Unite { get; set; }

    }
}
