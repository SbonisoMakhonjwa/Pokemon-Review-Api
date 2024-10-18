using Microsoft.EntityFrameworkCore;
using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Data.DataTransferObject;
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

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper()
            == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _apiDbContext.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _apiDbContext.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _apiDbContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };
            _apiDbContext.Add(pokemonCategory);

            _apiDbContext.Add(pokemon);

            return Save();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, 
            Pokemon pokemon)
        {
            _apiDbContext.Update(pokemon);
            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _apiDbContext.Remove(pokemon);
            return Save();
        }

        public bool Save()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
