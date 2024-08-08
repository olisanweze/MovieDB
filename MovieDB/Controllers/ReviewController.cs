using Microsoft.AspNetCore.Mvc;
using MovieDB.DAL;
using MovieDB.Models;
using System.Linq;

namespace MovieDB.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MovieDBContext _context;

        public ReviewController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: Review
        public IActionResult Index()
        {
            var reviews = _context.Reviews.ToList();
            return View(reviews);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Review/Edit/5
        public IActionResult Edit(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(review);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Review/Delete/5
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
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
