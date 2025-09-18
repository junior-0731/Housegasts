using pelicualasAPI.validaciones;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // DTO para devolver datos de producto al frontend
    // Este es el formato que se envia cuando se obtiene un producto
    public class ProductoDTO
    {
        public int Id { get; set; } // ID unico del producto
        public required string Name { get; set; } // Nombre del producto (obligatorio)
        public int Category { get; set; } // Categoria del producto
        public int Unite { get; set; } // Cantidad de unidades


    }
}
