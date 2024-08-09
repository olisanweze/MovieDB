using Microsoft.AspNetCore.Mvc;
using MovieDB.Models;
using MovieDB.BLL;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace MovieDB.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MovieService _movieService;

        public MovieController(IHttpClientFactory httpClientFactory, MovieService movieService)
        {
            _httpClientFactory = httpClientFactory;
            _movieService = movieService;
        }

        private async Task<Movie> GetMovieAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://freetestapi.com/api/v1/movies");
            response.EnsureSuccessStatusCode();

            var productJson = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<List<Movie>>(productJson);

            // Select a random movie
            var random = new Random();
            var randomIndex = random.Next(movies.Count);
            return movies[randomIndex];
        }

        public IActionResult Index()
        {
            List<Movie> movies = _movieService.GetMovies();
            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var movieFromAPI = await GetMovieAsync();

            // Destrucuture the movie.
            Movie newMovie = new Movie
            {
                title = movieFromAPI.title,
                year = movieFromAPI.year,
            //    genre = movieFromAPI.genre,
                rating = movieFromAPI.rating,
             //   director = movieFromAPI.director,
              //  actors = movieFromAPI.actors,
                plot = movieFromAPI.plot,
                poster = movieFromAPI.poster
            };

            // Store this movie in DB
            await _movieService.AddMovie(newMovie);

            // Return back to the index view
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = _movieService.GetMovie(id);
            if (movie != null)
            {
                await _movieService.DeleteMovie(id);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var movie = _movieService.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]

        public IActionResult ConfirmDelete()
        {
            return View();
        }
    }
}