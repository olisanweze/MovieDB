using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDB.BLL;
using MovieDB.Models;

namespace MovieDB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Create()
        {
            return View();
        }

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
    }
}
