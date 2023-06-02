using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Dto;
using SistemAdminProducts.Repository.IRepository;
using System.Net;

namespace SistemAdminProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplier _supplierRepository;
        private readonly IMapper _mapper;
        protected DefaultResponse _response;
        public SupplierController(IMapper mapper, ISupplier supplier)
        {
            _mapper = mapper;
            _supplierRepository = supplier;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetSuppliers()
        {
            await Console.Out.WriteLineAsync("Pidiendo los suppliers");
            try
            {
                _response.Data = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierRepository.GetAll());
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
                StatusCode(500, _response);
            }
            return Ok(_response);
        }

        [HttpGet("id:int", Name = "GetSupplierById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetSupplierById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            try
            {
                var supplier = await _supplierRepository.Get(v => v.Id == id);
                if(supplier == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "Proveedor no encontrado" };
                    return NotFound(_response);
                }
                _response.Data = _mapper.Map<SupplierDto>(supplier);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }

        [HttpDelete("id:int", Name = "DeleteSupplierById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> DeleteSupplierById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            try
            {
                var supplier = await _supplierRepository.Get(p => p.Id == id);
                if(supplier == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "Proveedor no encontrado" };
                    return NotFound(_response);
                }
                await _supplierRepository.Delete(supplier);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = supplier;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PostSupplier([FromBody] SupplierCreateDto newSupplier)
        {
            if (!ModelState.IsValid)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return BadRequest(_response);
            }
            try
            {
                var existSupplier = await _supplierRepository.Get(p => p.Name.ToLower() == newSupplier.Name.ToLower());
                if(existSupplier != null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string> { $"El proveedor {newSupplier.Name} ya esta registrado" };
                    return BadRequest(_response);
                }
                var supplier = _mapper.Map<Supplier>(newSupplier);
                supplier.CreateAt = DateTime.Now;
                supplier.UpdateAt = DateTime.Now;
                await _supplierRepository.Create(supplier);
                _response.Data = _mapper.Map<SupplierDto>(supplier);
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }

        [HttpPut("id:int", Name = "PutSupplierById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PutSupplierById(int id, [FromBody] SupplierUpdateDto supplierToUpdate)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            if (!ModelState.IsValid)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                return BadRequest(_response);
            }
            try
            {
                var supplier = await _supplierRepository.Get(p => p.Id == id);
                if (supplier == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "El proveedor no existe" };
                    return NotFound(_response);
                }

                supplier = _mapper.Map<Supplier>(supplierToUpdate);
                await _supplierRepository.Update(supplier);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = supplier;
            }
            catch (Exception ex)
            {
                _response.Ok = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
            return Ok(_response);
        }

    }
}
