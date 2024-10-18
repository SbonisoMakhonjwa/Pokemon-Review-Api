using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int CategoryId);
        ICollection<Pokemon> GetPokemonByCategoryId(int CategoryId);
        bool CategoryExists(int CategoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
