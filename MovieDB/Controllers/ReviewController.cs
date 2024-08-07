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

    }
}
