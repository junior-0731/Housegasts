using pelicualasAPI.validaciones;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // DTO para crear o actualizar un producto
    // Este es el formato que se recibe desde el frontend
    public class ProductoCreacionDTO
    {
            // Validacion personalizada que verifica que la primera letra sea mayuscula
            [PrimeraLetraMayusculaAtribute]
            // Validacion que hace que el campo sea obligatorio
            [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "El campo {0} es Obligatorio Chaval")]
            // Validacion que limita la longitud a 10 caracteres
            [StringLength(10, ErrorMessage = "El campo debe de tener {1} caracateres")]
            public required string Name { get; set; } // Nombre del producto (obligatorio)
            public int Category { get; set; } // Categoria del producto
            public int Unite { get; set; } // Cantidad de unidades

    }
}
