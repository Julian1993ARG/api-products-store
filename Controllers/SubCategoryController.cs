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
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategory _subCategoryRepository;
        private readonly ICategory _categoryRepository;
        private readonly IMapper _mapper;
        protected DefaultResponse _response;
        public SubCategoryController(IMapper mapper, ISubCategory subCategory, ICategory category)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategory;
            _categoryRepository = category;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetSubCategories()
        {
            try
            {
                _response.Data = _mapper.Map<IEnumerable<SubCategoryDto>>(await _subCategoryRepository.GetAll());
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

        [HttpGet("id:int", Name = "GetSubCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetSubCategoryById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            try
            {
                var category = await _subCategoryRepository.Get(v => v.Id == id);
                if(category == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "Categoria no encontrada" };
                    return NotFound(_response);
                }
                _response.Data = _mapper.Map<SubCategoryDto>(category);
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

        [HttpDelete("id:int", Name = "DeleteSubCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> DeleteSubCategoryById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            try
            {
                var category = await _subCategoryRepository.Get(p => p.Id == id);
                if(category == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "Categoria no encontrada" };
                    return NotFound(_response);
                }
                await _subCategoryRepository.Delete(category);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = category;
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
        public async Task<ActionResult<DefaultResponse>> PostSubCategory([FromBody] SubCategoryCreateDto newSubCategory)
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
                var existCategory = await _categoryRepository.Get(p => p.Id == newSubCategory.CategoryId);
                if (existCategory == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string> { $"La categoria no existe" };
                    return BadRequest(_response);
                }
                var existSubCategory = await _subCategoryRepository.Get(p => p.Name.ToLower() == newSubCategory.Name.ToLower());
                if(existSubCategory != null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string> { $"La sub categoria {newSubCategory.Name} ya esta registrada" };
                    return BadRequest(_response);
                }
                var category = _mapper.Map<SubCategory>(newSubCategory);
                category.CreateAt = DateTime.Now;
                category.UpdateAt = DateTime.Now;
                await _subCategoryRepository.Create(category);
                _response.Data = _mapper.Map<SubCategoryDto>(category);
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

        [HttpPut("id:int", Name = "PutSubCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PutSubCategoryById(int id, [FromBody] SubCategoryUpdateDto categoryUpdate)
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
                var category = await _subCategoryRepository.Get(p => p.Id == id, traked:false);
                if (category == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "La categoria no existe" };
                    return NotFound(_response);
                }

                category = _mapper.Map<SubCategory>(categoryUpdate);
                await _subCategoryRepository.Update(category);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Data = category;
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
