using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieDB.BLL;
using MovieDB.Models;

namespace MovieDB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        //private readonly ILogger<ReviewController> _logger;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            List<Review> reviews = new List<Review>();
            return View(reviews);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //          //  await _reviewService.AddReviewAsync(review);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "An error occurred while adding the review.");
        //            ModelState.AddModelError(string.Empty, "An error occurred while adding the review.");
        //        }
        //    }

        //    return View(review); // the command NOTE: *CHECKPOINT*
        //}

        // GET: Review/Index

        // GET: Review/Edit/{id}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    try
        //    {
        //        var review = await _reviewService.GetReviewAsync(id);
        //        if (review == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(review);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"An error occurred while fetching the review with ID {id}.");
        //        return View("Error");
        //    }
        //}

        // POST: Review/Edit/{id}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Review review)
        //{
        //    if (id != review.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //         //   await _reviewService.UpdateReviewAsync(review);
        //            return RedirectToAction("Index");
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "An error occurred while updating the review.");
        //            ModelState.AddModelError(string.Empty, "An error occurred while updating the review.");
        //        }
        //    }

        //    return View(review);
        //}

        // GET: Review/Delete/{id}
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //     //   var review = await _reviewService.GetReviewAsync(id);
        //       // if (review == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(review);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"An error occurred while fetching the review with ID {id}.");
        //        return View("Error");
        //    }
        //}

        // POST: Review/Delete/{id}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //     //   await _reviewService.DeleteReviewAsync(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"An error occurred while deleting the review with ID {id}.");
        //        return View("Error");
        //    }
        //}
    }
}
