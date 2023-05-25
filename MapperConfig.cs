using AutoMapper;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Dto;

namespace SistemAdminProducts
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Products,ProductCreateDto>().ReverseMap();
            CreateMap<Products,ProductUpdateDto>().ReverseMap();
        }
    }
}
