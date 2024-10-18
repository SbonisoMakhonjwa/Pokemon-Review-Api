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

        public bool CategoryExists(int CategoryId)
        {
            return _apiDbContext.Categories.Any(c => c.Id == CategoryId);
        }

        public bool CreateCategory(Category category)
        {
            _apiDbContext.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _apiDbContext.Remove(category);
            return Save();
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

        public bool Save()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _apiDbContext.Update(category);
            return Save();
        }
    }
}
