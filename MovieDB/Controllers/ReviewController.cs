using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            // Fetch the list of movies
            var movies = _movieService.GetMovies();

            // Transform the movie list to SelectListItem list. Because, this is the "format" that the Select HTML tag works with in asp.net
            //IEnumerable<SelectListItem> movieSelectList = movies.Select(movie => new SelectListItem
            //{
            //    Value = movie.movieID.ToString(), // Use the movie's ID as the value
            //    Text = movie.title // Use the movie's title as the display text
            //});


            // Create a View Model object to pass
            ReviewViewModel reviewViewModel = new ReviewViewModel()
            {
                // Pass the list of movies as SelectListItem
                Movies = new SelectList(movies, "movieID", "title"),
                Comment = ""    
            };

            return View(reviewViewModel);
        }


        [HttpPost]
        public IActionResult Create(ReviewViewModel reviewViewModel)
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
            //if (ModelState.IsValid)
            //{
            //    _reviewService.AddReview(review);
            //    return RedirectToAction("Index");
            //}
            return View();
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
