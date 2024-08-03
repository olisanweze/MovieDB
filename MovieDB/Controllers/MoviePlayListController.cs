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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayList playList)
        {
            if (ModelState.IsValid)
            {
                _playListService.AddPlayList(playList);
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
