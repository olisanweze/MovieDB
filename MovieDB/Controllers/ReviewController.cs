using Microsoft.AspNetCore.Mvc;
using MovieDB.BLL;
using MovieDB.DAL;
using MovieDB.Models;
using System;
using System.Collections.Generic;

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

        // GET: Review/Create
        public IActionResult Create()
        {
            // Create a new review object
            Review review = new Review();

            // Get all movies to populate the dropdown menu
            List<Movie> movies = _movieService.GetMovies();

            // Assign movies to the ViewBag to use in the View
            ViewBag.Movies = movies;

            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _reviewService.AddReview(review);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the review.");
                }
            }

            // Repopulate the movies in case the form is redisplayed
            ViewBag.Movies = _movieService.GetMovies();

            // If we got this far, something failed; redisplay the form
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review != null)
            {
                _reviewService.DeleteReview(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}