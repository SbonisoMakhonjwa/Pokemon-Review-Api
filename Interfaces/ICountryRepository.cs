using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int countryId);
        Country GetCountryByOwnerId(int ownerId);
        bool CountryExists(int countryId);
    }
}
