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
            // Retrieve the selected movie
            Movie movieSelected = _movieService.GetMovie(reviewViewModel.SelectedMovieId);

            // Initialize a review
            Review review = new Review
            {
                MovieName = movieSelected.title,
                Star = reviewViewModel.Star,
                Comment = reviewViewModel.Comment,
                Movie = movieSelected
            };

            // Ensure Reviews collection is initialized in case this is the first time this movie is getting a review
            if (movieSelected.Reviews == null)
            {
                movieSelected.Reviews = new List<Review>();
            }

            // Associate the review with the movie
            movieSelected.Reviews.Add(review);

            if (ModelState.IsValid)
            {
                _reviewService.AddReview(review);
                return RedirectToAction("Index");
            }

            return View(reviewViewModel);
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var review = _reviewService.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost]

        public IActionResult ConfirmDelete()
        {
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
            var reviewUpdateViewModel = new ReviewUpdateViewModel
            {
                ReviewId = review.ReviewId,
                MovieName = review.MovieName,
                movieID = review.movieID,
                Star = review.Star,
                Comment = review.Comment
            };

            return View(reviewUpdateViewModel);
        }

        [HttpPost]
        public IActionResult Update(ReviewUpdateViewModel reviewUpdateViewModel)
        {
            var movie = _movieService.GetMovie(reviewUpdateViewModel.movieID);

            var review = new Review
            {
                ReviewId = reviewUpdateViewModel.ReviewId,
                MovieName = reviewUpdateViewModel.MovieName,
                movieID = reviewUpdateViewModel.movieID,
                Star = reviewUpdateViewModel.Star,
                Comment = reviewUpdateViewModel.Comment,
                Movie = movie
            };

            if (ModelState.IsValid)
            {
                
                _reviewService.UpdateReview(review);
                return RedirectToAction("Index");
            }

            reviewUpdateViewModel = new ReviewUpdateViewModel
            {
                ReviewId = review.ReviewId,
                MovieName = review.MovieName,
                movieID = review.movieID,
                Star = review.Star,
                Comment = review.Comment
            };

            return View(reviewUpdateViewModel);
        }
    }
}
