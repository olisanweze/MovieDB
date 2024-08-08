using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDB.BLL;
using MovieDB.DAL;
using MovieDB.Models;
using System.Threading.Tasks;
using System.Security.Claims;


namespace MovieDB.Controllers
{
    [Authorize]
    public class MoviePlayListController : Controller
    {
        private readonly MoviePlayListService _moviePlayListService;
        private readonly PlayListService _playListService;
        private readonly MovieService _movieService;

        public MoviePlayListController (PlayListService playListService, MoviePlayListService moviePlayListService, MovieService movieService)
        {
            _playListService = playListService;
            _moviePlayListService = moviePlayListService;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email); 
            var playLists = _playListService.GetPlayLists();
            foreach (var playList in playLists)
            {
                playList.MoviePlayLists = _moviePlayListService.GetMoviePlayListsByPlayListId(playList.PlayListId).ToList();
            }
            return View(playLists);
        }

        public IActionResult Create()
        {
            
            ViewData["movies"] = _movieService.GetMovies();
            return View();
        }

        [HttpPost]
        public IActionResult Create(PlayList playList, int[] selectedMovieIds)
        {
            if (ModelState.IsValid)
            {
                _playListService.AddPlayList(playList);
                var movies = _movieService.GetMovies();

                foreach (var movieId in selectedMovieIds)
                {
                    var movie = movies.FirstOrDefault(m => m.movieID == movieId);
                    var moviePlayList = new MoviePlayList
                    {
                        PlayListId = playList.PlayListId,
                        MovieId = movieId,
                        MovieName = movie.title
                    };

                    _moviePlayListService.AddMoviePlayList(moviePlayList);
                }

                return RedirectToAction("Index");
            }

            ViewData["movies"] = _movieService.GetMovies();

            return View(playList);
        }

        public IActionResult Update(int playListId)
        {
            var playList = _playListService.GetPlayList(playListId);


            if (playList == null)
            {
                return NotFound();
            }
            ViewData["movies"] = _movieService.GetMovies(); ;
            ViewData["moviesInPlayList"] = _moviePlayListService.GetMoviePlayListsByPlayListId(playListId).Select(m => m.MovieId).ToList(); ;
            return View(playList);
        }

        [HttpPost]
        public IActionResult Update(PlayList playList, int[] selectedMovieIds)
        {
            if (ModelState.IsValid)
            {
                _playListService.UpdatePlayList(playList);

                var moviesInPlayList = _moviePlayListService.GetMoviePlayListsByPlayListId(playList.PlayListId).Select(m => m.MovieId).ToList();
                
                var movieListCreate = selectedMovieIds.Except(moviesInPlayList).ToList();
                var movieListDelete = moviesInPlayList.Except(selectedMovieIds).ToList();
                var movies = _movieService.GetMovies();

                foreach (var movieId in movieListCreate)
                {
                    var movie = movies.FirstOrDefault(m => m.movieID == movieId);
                    var moviePlayList = new MoviePlayList
                    {
                        PlayListId = playList.PlayListId,
                        MovieId = movieId,
                        MovieName = movie.title
                    };

                    _moviePlayListService.AddMoviePlayList(moviePlayList);
                }

                foreach (var movieId in movieListDelete)
                {
                    _moviePlayListService.DeleteMoviePlayListByIds(playList.PlayListId, movieId);
                }


                return RedirectToAction("Index");
            }

            ViewData["movies"] = _movieService.GetMovies(); ;
            ViewData["moviesInPlayList"] = _moviePlayListService.GetMoviePlayListsByPlayListId(playList.PlayListId).Select(m => m.MovieId).ToList(); ;

            return View(playList);
        }
         
        public IActionResult Delete(int playListId)
        {
            var playList = _playListService.GetPlayList(playListId);
            playList.MoviePlayLists = _moviePlayListService.GetMoviePlayListsByPlayListId(playList.PlayListId).ToList();
            return View(playList);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int playListId)
        {
            _playListService.DeletePlayList(playListId);
            _moviePlayListService.DeleteMoviePlayList(playListId);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed()
        {
            _moviePlayListService.DeleteAllMoviePlayLists();
            _playListService.DeleteAllPlayLists();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            return View();
        }

    }
}
