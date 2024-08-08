using Microsoft.AspNetCore.Mvc;
using MovieDB.DAL;
using MovieDB.Models;
using System.Linq;

namespace MovieDB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        private readonly MovieService _movieService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Create()
        {
            Review review = new Review();

            // Create a View Model object to pass
            // Get all the movies you want to put in the dropdown menu
            List<Movie> movies = _movieService.GetMovies();
            // Assign those movies to the movies list in the view model


            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    object value = await _reviewService.AddReviewAsync(review);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the review.");
                }
            }

            // If we got this far, something failed; this will redisplay the form.
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
