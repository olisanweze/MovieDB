using Microsoft.EntityFrameworkCore;
using MovieDB.DAL;
using MovieDB.Models;

namespace MovieDB.BLL
{
    public class MoviePlayListService
    {
        private readonly MoviePlayListDAL _movieplaylistDAL;

        public MoviePlayListService(MoviePlayListDAL movieplaylistDAL)
        {
            _movieplaylistDAL = movieplaylistDAL;
        }

        public List<MoviePlayList> GetMoviePlayLists()
        {
            return _movieplaylistDAL.GetMoviePlayLists();
        }

        public MoviePlayList GetMoviePlayList(int id, int movieId)
        {
            return _movieplaylistDAL.GetMoviePlayList(id, movieId);
        }

        public List<MoviePlayList> GetMoviePlayListsByPlayListId(int playListId)
        {
            return _movieplaylistDAL.GetMoviePlayListsByPlayListId(playListId);
        }

        public void AddMoviePlayList(MoviePlayList movieplaylist)
        {
            _movieplaylistDAL.AddMoviePlayList(movieplaylist);
        }

        public void UpdateMoviePlayList(MoviePlayList movieplaylist)
        {
            _movieplaylistDAL.UpdateMoviePlayList(movieplaylist);
        }

        public void DeleteMoviePlayList(int id)
        {
            _movieplaylistDAL.DeleteMoviePlayList(id);
        }

        public void DeleteMoviePlayListByIds(int playListId, int movieId)
        {
            _movieplaylistDAL.DeleteMoviePlayListByIds(playListId, movieId);
        }
        public void DeleteAllMoviePlayLists()
        {
            _movieplaylistDAL.DeleteAllMoviePlayLists();
        }

    }
}
