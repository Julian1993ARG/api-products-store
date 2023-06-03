using AutoMapper;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Dto;

namespace SistemAdminProducts
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Products,ProductDto>().ReverseMap();
            CreateMap<Products,ProductCreateDto>().ReverseMap();
            CreateMap<Products,ProductUpdateDto>().ReverseMap();

            CreateMap<Supplier,SupplierDto>().ReverseMap();
            CreateMap<Supplier,SupplierCreateDto>().ReverseMap();
            CreateMap<Supplier,SupplierUpdateDto>().ReverseMap();

            CreateMap<Category,CategoryUpdateDto>().ReverseMap();
            CreateMap<Category,CategoryCreateDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();

            CreateMap<SubCategory,SubCategoryUpdateDto>().ReverseMap();
            CreateMap<SubCategory,SubCategoryCreateDto>().ReverseMap();
            CreateMap<SubCategory,SubCategoryDto>().ReverseMap();
        }
    }
}
