using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PakemonReviewWebAPI.Data;
using PakemonReviewWebAPI.Interfaces;
using PakemonReviewWebAPI.Models;

namespace PakemonReviewWebAPI.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly IMapper _mapper;

        public ReviewerRepository(ApiDbContext context, IMapper mapper)
        {
            _apiDbContext = context;
            _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _apiDbContext.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _apiDbContext.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _apiDbContext.Reviewers.Where(r => r.Id == reviewerId).
                Include(e => e.Reviews).FirstOrDefault()
                ?? throw new NullReferenceException();
           
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _apiDbContext.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _apiDbContext.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _apiDbContext.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _apiDbContext.Update(reviewer);
            return Save();
        }
    }
}
