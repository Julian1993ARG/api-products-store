using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemAdminProducts.Repository.IRepository;

namespace SistemAdminProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper, IProduct product)
        {
            _mapper = mapper;
            _productRepository = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }
    }
}
