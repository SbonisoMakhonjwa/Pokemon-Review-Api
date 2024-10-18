using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PakemonReviewWebAPI.Data.DataTransferObject;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;
using PakemonReviewWebAPI.Repository;

namespace PakemonReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public PokemonController(IPokemonRepository pokemonRepository, 
            IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>
                (_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpGet("{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokemonId)
        { 
            if(!_pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>
                (_pokemonRepository.GetPokemon(pokemonId));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);
        }

        [HttpGet("{pokemonId}/rating")]
        public IActionResult GetPokemonRating(int pokemonId)
        {
            if (!_pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var rating =  _pokemonRepository.GetPokemonRating(pokemonId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId
            ,[FromQuery] int categoryId
            ,[FromBody] PokemonDto pokemon)
        {
            if(pokemon == null)
            {
                return BadRequest(ModelState);
            }

            var pokemons = _pokemonRepository.GetPokemons()
                .Where(p => p.Name.Trim().ToUpper() == 
                pokemon.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if(pokemons != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(442, ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemon);

            if(!_pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong" +
                    " while creating pokemon");
                return StatusCode(500, ModelState);
            }

            return Ok("Created Successfully");
        }

        [HttpPut("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokemonId,
            [FromQuery] int ownerId,
            [FromQuery] int categoryId,
            [FromBody] PokemonDto pokemon)
        {
            if(pokemon == null)
            { 
                return BadRequest(ModelState); 
            }

            if(pokemonId != pokemon.Id)
            {
                return BadRequest(ModelState);
            }

            if(!_pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemon);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_pokemonRepository.UpdatePokemon(ownerId, categoryId
                , pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong" +
                    " while updating pokemon");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokemonId)
        {
            if (!_pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var pokemonToDelete = _pokemonRepository.GetPokemon(pokemonId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong " +
                    "while deleting pokemon");
                return StatusCode(500, ModelState);
            }

            var reviews = _reviewRepository.GetReviewsOfAPokemon(pokemonId);

            if (_reviewRepository == null)
            {
                ModelState.AddModelError("", "Review repository is " +
                    "not initialized therefore no review to delete");
                return StatusCode(500, ModelState);
            }

            if (reviews != null && reviews.Any())
            {
                if (!_reviewRepository.DeleteReviews(reviews.ToList()))
                {
                    ModelState.AddModelError("", "Something went wrong " +
                        "while deleting reviews of this particular pokemon");
                    return StatusCode(500, ModelState);
                }
            }

            return Ok("Successfully Deleted");
        }
    }
}
