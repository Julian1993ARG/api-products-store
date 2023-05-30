using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ISupplier _supplierRepository;
        private readonly IMapper _mapper;
        protected DefaultResponse _response;
        public ProductsController(IMapper mapper, ISupplier supplier, IProduct product)
        {
            _productRepository = product;
            _supplierRepository = supplier;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetProducts([FromQuery] bool withSupplier = true, [FromQuery] int? supplierId = null)
        {
            try
            {
                if (withSupplier) _response.Data = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll(include: query => IncludeSupplier(query, supplierId)));
                else _response.Data = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
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
            Guard.Against.NegativeOrZero(id, nameof(id));
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
            Guard.Against.NullOrWhiteSpace(upcCode, nameof(upcCode));
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

        [HttpGet("details:string", Name = "GetProductsByDescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetProductsByDescription(string details)
        {
           Guard.Against.NullOrWhiteSpace(details, nameof(details));
            if (details.Length < 3)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = new List<string> { "El detalle debe tener al menos 3 caracteres" };
                return _response;
            }
            try
            {
                IEnumerable<Products> products = await _productRepository.GetProductsByName(details);
                if(!products.Any())
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "No se encontraron productos" };
                    return _response;
                }
                _response.Data = _mapper.Map<IEnumerable<ProductDto>>(products);
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
            Guard.Against.NegativeOrZero(id, nameof(id));
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
            Guard.Against.NullOrWhiteSpace(upc, nameof(upc));
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
                    _response.ErrorMessage = new List<string> { $"El UpcCode enviado ya esta registrado para el producto {existProduct.Description}" };
                    return _response;
                }
                var product = _mapper.Map<Products>(newProduct);
                product.Description = product.Description.ToUpper();
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
            Guard.Against.NegativeOrZero(id, nameof(id));
            Guard.Equals(productToUpdate.Id, id);
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
                    _response.ErrorMessage = new List<string> { $"El UpcCode enviado ya esta registrado para el producto {existProduct.Description}" };
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

        [HttpPatch("idSupplier:int", Name ="PatchGrupPriceByParams")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PatchGrupPriceByParams(int idSupplier, [FromQuery] double percentage)
        {
            Guard.Against.NegativeOrZero(idSupplier, nameof(idSupplier));
            Guard.Against.NegativeOrZero(percentage, nameof(percentage));
            try
            {
                var supplier = await _supplierRepository.Get(s => s.Id == idSupplier);
                if(supplier == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El proveedor no existe" };
                    return _response;
                }
                await  _productRepository.UpdateProductsPriceBySupplierId(idSupplier, percentage);
                _response.Data = new List<string> { $"Todos los precios del proveedor {supplier.Name} fueron aumentados un {percentage}% " };
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        // DELEGATES METHODS


        readonly Func<IQueryable<Products>, int?, IQueryable<Products>> IncludeSupplier = (query, supplierId) =>
            query.Include(product => product.Supplier)
            .Where(products => (products.Supplier != null && supplierId != null && products.SupplierId == supplierId) || (supplierId == null && products.Supplier == products.Supplier))
            .Select(product => new Products
            {
                Id = product.Id,
                Description = product.Description,
                UpcCode = product.UpcCode,
                CostPrice = product.CostPrice,
                Proffit = product.Proffit,
                SupplierId = product.SupplierId,
                Supplier = product.Supplier != null ? new Supplier
                {
                    Id = product.Supplier.Id,
                    Name = product.Supplier.Name,
                    Address = product.Supplier.Address,
                    Phone = product.Supplier.Phone,
                    Email = product.Supplier.Email
                } : null
            });

        // TODO: Ver la forma de implementar este metodo en IProducts
        readonly Func<IQueryable<Products>, int, double, IQueryable<Products>> UpdateProductsPriceBySupplierId = (query, supplierId, percentage) =>
        (IQueryable<Products>)query.Where(produt => produt.SupplierId == supplierId)
        .ExecuteUpdateAsync(p => p.SetProperty(p => p.CostPrice,  p => p.CostPrice * percentage));
    }
}
