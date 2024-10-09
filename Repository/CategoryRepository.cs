using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public CategoryRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public bool CategoriesExists(int CategoryId)
        {
            return _apiDbContext.Categories.Any(c => c.Id == CategoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _apiDbContext.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int CategoryId)
        {
            return _apiDbContext.Categories.Where(c => c.Id == CategoryId)
                .FirstOrDefault() ?? throw new NullReferenceException();
        }

        public ICollection<Pokemon> GetPokemonByCategoryId(int CategoryId)
        {
            return _apiDbContext.PokemonCategories
                .Where(c => c.CategoryId == CategoryId)
                .Select(p => p.Pokemon).ToList();
        }
    }
}
