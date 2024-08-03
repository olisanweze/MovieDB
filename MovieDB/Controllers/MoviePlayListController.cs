using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDB.BLL;
using MovieDB.DAL;
using MovieDB.Models;
using System.Threading.Tasks;


namespace MovieDB.Controllers
{
    public class MoviePlayListController : Controller
    {
        private readonly MoviePlayListService _moviePlayListService;
        private readonly PlayListService _playListService;
        List<Movie> movies = new List<Movie>{ new Movie {
            Title = "Movie Title",
            Year = 2023,
            Genre = "Drama",
            Rating = 8.5,
            MovieId = 1,
            Director = "Director Name",
            Actors = "Actor Names",
            Plot = "Movie Plot",
            Poster = "http://example.com/poster.jpg"
            },
            new Movie {
            Title = "Movie Title 1",
            Year = 2024,
            Genre = "Drama - 1",
            Rating = 8.0,
             MovieId = 2,
            Director = "Director Name 1",
            Actors = "Actor Names 1",
            Plot = "Movie Plot 1",
            Poster = "http://example.com/poster1.jpg"
            }
            };

        public MoviePlayListController (PlayListService playListService, MoviePlayListService moviePlayListService)
        {
            _playListService = playListService;
            _moviePlayListService = moviePlayListService;
        }

        public async Task<IActionResult> Index()
        {
            var playLists = _playListService.GetPlayLists();
            return View(playLists);
        }

        public IActionResult Create()
        {
            
            ViewData["movies"] = movies;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayList playList, int[] selectedMovieIds)
        {
            if (ModelState.IsValid)
            {
                _playListService.AddPlayList(playList);

                foreach (var movieId in selectedMovieIds)
                {
                    var movie = movies.FirstOrDefault(m => m.MovieId == movieId);
                    var moviePlayList = new MoviePlayList
                    {
                        PlayListId = playList.PlayListId,
                        MovieId = movieId,
                        MovieName = movie.Title
                    };

                    _moviePlayListService.AddMoviePlayList(moviePlayList);
                }

                return RedirectToAction("Index");
            }

            return View(playList);
        }

        public async Task<IActionResult> Update(int playListId)
        {
            var moviePlayList =  _playListService.GetPlayList(playListId);
            return View(moviePlayList);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PlayList playList)
        {
            if (ModelState.IsValid)
            {
                _playListService.UpdatePlayList(playList);
                return RedirectToAction("Index");
            }
            return View(playList);
        }
         
        public async Task<IActionResult> Delete(int playListId)
        {
            var playList = _playListService.GetPlayList(playListId);
            return View(playList);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int playListId)
        {
            _playListService.DeletePlayList(playListId);
            return RedirectToAction("Index");
        }
    }
}
