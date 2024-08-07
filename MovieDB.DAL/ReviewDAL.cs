using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MovieDB.Models;

namespace MovieDB.DAL
{
    public class ReviewDAL : IdentityDbContext<IdentityUser> // IdentityDbContext<IdentityUser
    {
        private readonly MovieDBContext _context;

        public ReviewDAL(MovieDBContext context)
        {
            _context = context;
        }

        public List<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Find(id);
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}