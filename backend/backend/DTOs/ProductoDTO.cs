using pelicualasAPI.validaciones;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Category { get; set; }
        public int Unite { get; set; }


    }
}
