using AutoMapper;
using backend.DTOs;
using pelicualasAPI.Entidades;

namespace backend.utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            configurarMapeoProductos();
        }
        private void configurarMapeoProductos()
        {
            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();

        }


    }
}
