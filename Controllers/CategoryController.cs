using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PakemonReviewWebAPI.Data.DataTransferObject;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, 
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>
                (_categoryRepository
                .GetCategories());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.IsValid);
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if(!_categoryRepository.CategoriesExists(categoryId))
            {
                return NotFound();
            }
            var categories = _mapper.Map<CategoryDto>(_categoryRepository
                .GetCategory(categoryId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.IsValid);
            }

            return Ok(categories);

        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(
                _categoryRepository.GetPokemonByCategoryId(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.IsValid);
            }

            return Ok(pokemons);
        }
    }
}
