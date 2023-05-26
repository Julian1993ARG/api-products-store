using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Dto;
using SistemAdminProducts.Repository.IRepository;
using System.Net;

namespace SistemAdminProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _productRepository;
        private readonly IMapper _mapper;
        protected DefaultResponse _response;
        public ProductsController(IMapper mapper, IProduct product)
        {
            _mapper = mapper;
            _productRepository = product;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetProducts()
        {
            try
            {
                _response.Data = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetProductBy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetProductById(int id)
        {
            if(id.GetType() != typeof(int))
            {
                _response.Ok = false;
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.ErrorMessage = new List<string> { "El id debe ser un entero" };
                return _response;
            }
            try
            {
                var product = await _productRepository.Get(v => v.Id == id);
                if(product == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El producto no existe" };
                    return _response;
                }
                _response.Data = _mapper.Map<ProductDto>(product);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("upc:string", Name = "GetProductByUpcCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetProductByUpcCode(string upcCode)
        {
            if (upcCode.GetType() != typeof(string))
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = new List<string> { "El codigo debe ser un string" };
                return _response;
            }
            try
            {
                var product = await _productRepository.Get(v => v.UpcCode == upcCode);
                if (product == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El producto no existe" };
                    return _response;
                }
                _response.Data = _mapper.Map<ProductDto>(product);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("id:int", Name = "DeleteProductById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> DeleteProductById(int id)
        {
            if (id.GetType() != typeof(int))
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = new List<string> { "El id debe ser un entero" };
                return _response;
            }
            try
            {
                var product = await _productRepository.Get(p => p.Id == id);
                if(product == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El producto no existe" };
                    return _response;
                }
                await _productRepository.Delete(product);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = product;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("upc:string", Name = "DeleteByUpcCode")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> DeleteByUpcCode(string upc)
        {
            if (upc.GetType() != typeof(int))
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = new List<string> { "El upcCode debe ser string" };
                return _response;
            }
            try
            {
                var product = await _productRepository.Get(p => p.UpcCode == upc);
                if (product == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El producto no existe" };
                    return _response;
                }
                await _productRepository.Delete(product);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = product;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PostProduct([FromBody] ProductCreateDto newProduct)
        {
            if (!ModelState.IsValid)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return _response;
            }
            try
            {
                var existProduct = await _productRepository.Get(p => p.UpcCode == newProduct.UpcCode);
                if(existProduct != null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string> { $"El UpcCode enviado ya esta registrado para el producto {existProduct.Decription}" };
                    return _response;
                }
                var product = _mapper.Map<Products>(newProduct);
                product.CreateAt = DateTime.Now;
                product.UpdateAt = DateTime.Now;
                await _productRepository.Create(product);
                _response.Data = _mapper.Map<ProductDto>(product);
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
                return _response;
            }
            return _response;
        }

        [HttpPut("id:int", Name = "PutProductById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PutProductById(int id, [FromBody] ProductUpdateDto productToUpdate)
        {
            if (id.GetType() != typeof(int))
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = new List<string> { "El id debe ser un entero" };
                return _response;
            }
            if (!ModelState.IsValid)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return _response;
            }
            try
            {
                var product = await _productRepository.Get(p => p.Id == id);
                if (product == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El producto no existe" };
                    return _response;
                }
                var existProduct = await _productRepository.Get(p => p.UpcCode == productToUpdate.UpcCode);
                if (existProduct != null && existProduct.Id != id)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string> { $"El UpcCode enviado ya esta registrado para el producto {existProduct.Decription}" };
                    return _response;
                }
                product = _mapper.Map(productToUpdate, product);
                await _productRepository.Update(product);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = product;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
