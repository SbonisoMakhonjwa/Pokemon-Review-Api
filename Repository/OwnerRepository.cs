using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public OwnerRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public bool CreateOwner(Owner owner)
        {
            _apiDbContext.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _apiDbContext.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _apiDbContext.Owners.Where(o => o.Id == ownerId)
                .FirstOrDefault() ?? throw new NullReferenceException();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _apiDbContext.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _apiDbContext.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _apiDbContext.PokemonOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _apiDbContext.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _apiDbContext.Update(owner);
            return Save();
        }
    }
    
}
