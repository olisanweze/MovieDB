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

        public IActionResult Index()
        {
            List<Review> reviews = _reviewService.GetReviews();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
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
