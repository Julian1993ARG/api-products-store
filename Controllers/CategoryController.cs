using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemAdminProducts.Models;
using SistemAdminProducts.Models.Context;
using SistemAdminProducts.Models.Dto;
using SistemAdminProducts.Repository.IRepository;
using System.Net;

namespace SistemAdminProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDdContext _db;
        protected DefaultResponse _response;
        public CategoryController(IMapper mapper, ICategory category, ApplicationDdContext db)
        {
            _mapper = mapper;
            _categoryRepository = category;
            _db = db;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetCategoryes()
        {
            try
            {
                var category = _db.Set<Category>();
                var data = await category.Select(c => new 
                {
                   c.Id,
                   c.Name,
                   SubCategories = c.SubCategories.Select(sb => new  { sb.Id, sb.Name }).ToList(),
                }).ToListAsync();
                _response.Data = data;
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

        [HttpGet("id:int", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> GetCategoryById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            try
            {
                var category = await _categoryRepository.Get(v => v.Id == id);
                if(category == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "Categoria no encontrada" };
                    return NotFound(_response);
                }
                _response.Data = _mapper.Map<CategoryDto>(category);
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

        [HttpDelete("id:int", Name = "DeleteCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> DeleteCategoryById(int id)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));
            try
            {
                var category = await _categoryRepository.Get(p => p.Id == id);
                if(category == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "Categoria no encontrada" };
                    return NotFound(_response);
                }
                await _categoryRepository.Delete(category);
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
        public async Task<ActionResult<DefaultResponse>> PostCategory([FromBody] CategoryCreateDto newCategory)
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
                var existSupplier = await _categoryRepository.Get(p => p.Name.ToLower() == newCategory.Name.ToLower());
                if(existSupplier != null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = new List<string> { $"La categoria {newCategory.Name} ya esta registrada" };
                    return BadRequest(_response);
                }
                var category = _mapper.Map<Category>(newCategory);
                category.CreateAt = DateTime.Now;
                category.UpdateAt = DateTime.Now;
                await _categoryRepository.Create(category);
                _response.Data = _mapper.Map<CategoryDto>(category);
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

        [HttpPut("id:int", Name = "PutCategoryById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResponse>> PutCategoryById(int id, [FromBody] CategoryUpdateDto categoryUpdate)
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
                var category = await _categoryRepository.Get(p => p.Id == id, traked:false);
                if (category == null)
                {
                    _response.Ok = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = new List<string> { "La categoria no existe" };
                    return NotFound(_response);
                }

                category = _mapper.Map<Category>(categoryUpdate);
                await _categoryRepository.Update(category);
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
