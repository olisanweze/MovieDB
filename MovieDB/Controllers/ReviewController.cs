using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDB.BLL;
using MovieDB.Models;

namespace MovieDB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        private readonly MovieService _movieService;

        public ReviewController(ReviewService reviewService, MovieService movieService)
        {
            _reviewService = reviewService;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            List<Review> reviews = _reviewService.GetReviews();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Review review = new Review();

            // Create a View Model object to pass
            // Get all the movies you want to put in the dropdown menu
            List<Movie> movies = _movieService.GetMovies();
            // Assign those movies to the movies list in the view model


            return View(review);
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            Review myReview = new Review
            {
                Movies = _movieService.GetMovies()
            };
            //{
            //    MovieName = review.MovieName,
            //    Star = review.Star,
            //    Comment = review.Comment
            //};
            if (ModelState.IsValid)
            {
                _reviewService.AddReview(review);
                return RedirectToAction("Index");
            }
            return View(review);
        }

        public IActionResult Delete(int id)
        {
            var review = _reviewService.GetReview(id);
            if (review != null)
            {
                _reviewService.DeleteReview(id);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var review = _reviewService.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost]
        public IActionResult Update(Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewService.UpdateReview(review);
                return RedirectToAction("Index");
            }
            return View(review);
        }
    }
}
