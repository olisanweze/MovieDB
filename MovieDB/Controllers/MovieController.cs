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
            var apiResponse = await client.GetAsync("https://api.andrespecht.dev/movies");

            apiResponse.EnsureSuccessStatusCode();

            var responseContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResult = JsonSerializer.Deserialize<APIResponse>(responseContent);
            if (apiResult != null && apiResult.success && apiResult.response != null && apiResult.response.Count > 0)
            {
                // Select a random movie from the response
                var random = new Random();
                var randomIndex = random.Next(apiResult.response.Count);
                return apiResult.response[randomIndex];
            }

            // Return null if the API call was unsuccessful or if no movies were found
            return null;
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
                description = movieFromAPI.description,
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

        [HttpGet]
        public IActionResult Update(int id)
        {
            var movie = _movieService.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.UpdateMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }
    }
}