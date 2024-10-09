using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly IMapper _mapper;

        public ReviewRepository(ApiDbContext apiDbContext, IMapper mapper)
        {
            _apiDbContext = apiDbContext;
            _mapper = mapper;
        }

        public ICollection<Review> GetReviews()
        {
            return _apiDbContext.Reviews.ToList();
        }

        public Review GetReview(int reviewId)
        {
            return _apiDbContext.Reviews.Where(r => r.Id == reviewId)
                .FirstOrDefault() ?? throw new NullReferenceException(); ;
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _apiDbContext.Reviews.Where(r => r.Pokeman.Id == pokeId).ToList();
        }

        public bool CreateReview(Review review)
        {
            _apiDbContext.Add(review);
            return Save();
        }

        public bool ReviewExists(int reviewId)
        {
            return _apiDbContext.Reviews.Any(r => r.Id == reviewId);
        }

        public bool UpdateReview(Review review)
        {
            _apiDbContext.Update(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
