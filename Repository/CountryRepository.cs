using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApiDbContext _apiDbContext;

        public CountryRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public bool CountryExists(int countryId)
        {
            return _apiDbContext.Countries.Any(c => c.Id == countryId);
        }

        public bool CreateCountry(Country country)
        {
            _apiDbContext.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _apiDbContext.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _apiDbContext.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int countryId)
        {
            return _apiDbContext.Countries.Where(c => c.Id == countryId)
                .FirstOrDefault()  ?? throw new InvalidOperationException();
        }

        public Country GetCountryByOwnerId(int ownerId)
        {
           return _apiDbContext.Owners.Where(o => o.Id == ownerId)
                .Select(c => c.Country).FirstOrDefault()
                ?? throw new InvalidOperationException();
        }

        public bool Save()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _apiDbContext.Update(country);
            return Save();
        }
    }
}
