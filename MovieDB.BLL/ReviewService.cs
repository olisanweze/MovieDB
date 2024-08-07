using MovieDB.DAL;
using MovieDB.Models;

namespace MovieDB.BLL
{
    public class ReviewService
    {
        private readonly ReviewDAL _reviewDAL;

        public ReviewService(ReviewDAL reviewDAL)
        {
            _reviewDAL = reviewDAL;
        }

        public List<Review> GetReviews()
        {
            return _reviewDAL.GetReviews();
        }

        public Review GetReview(int id)
        {
            return _reviewDAL.GetReview(id);
        }

        public void AddReview(Review review)
        {
            _reviewDAL.AddReview(review);
        }

        public void UpdateReview(Review review)
        {
            _reviewDAL.UpdateReview(review);
        }

        public void DeleteReview(int id)
        {
            _reviewDAL.DeleteReview(id);
        }

        public async Task<object> AddReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
