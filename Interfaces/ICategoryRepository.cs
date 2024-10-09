using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int CategoryId);
        ICollection<Pokemon> GetPokemonByCategoryId(int CategoryId);
        bool CategoriesExists(int CategoryId);
    }
}
