using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;
using System.Reflection;

namespace PakemonReviewWebAPI.Repository
{
    public class PokemonRepository: IPokemonRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public PokemonRepository(ApiDbContext apiDbContext) 
        {
            _apiDbContext = apiDbContext;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _apiDbContext.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int pokemonId)
        {
            return _apiDbContext.Pokemons.Where(p => p.Id == pokemonId).FirstOrDefault() 
                ?? throw new NullReferenceException();
        }

        public Pokemon GetPokemon(string name)
        {
            return _apiDbContext.Pokemons.Where(p => p.Name == name).FirstOrDefault() 
                ?? throw new NullReferenceException();
        }

        public decimal GetPokemonRating(int pokemonId)
        {
            var review = _apiDbContext.Reviews.Where(p => p.Pokeman.Id == pokemonId);
            
            if(review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }
        
        public bool PokemonExists(int pokemonId)
        {
            return _apiDbContext.Pokemons.Any(p => p.Id == pokemonId);
        }
    }
}
