using MovieDB.Models;

namespace MovieDB.DAL
{
    public class MoviePlayListDAL
    {
        private readonly MovieDBContext _context;

        public MoviePlayListDAL(MovieDBContext context)
        {
            _context = context;
        }

        public List<MoviePlayList> GetMoviePlayLists()
        {
            return _context.MoviePlayLists.ToList();
        }

        public MoviePlayList GetMoviePlayList(int id, int movieId)
        {
            return _context.MoviePlayLists.Find(id, movieId);
        }

        public List<MoviePlayList> GetMoviePlayListsByPlayListId(int playListId)
        {
            return _context.MoviePlayLists.Where(mpl => mpl.PlayListId == playListId).ToList();
        }

        public void AddMoviePlayList(MoviePlayList movieplaylist)
        {
            _context.MoviePlayLists.Add(movieplaylist);
            _context.SaveChanges();
        }

        public void UpdateMoviePlayList(MoviePlayList movieplaylist)
        {
            _context.MoviePlayLists.Update(movieplaylist);
            _context.SaveChanges();
        }

        public void DeleteMoviePlayList(int playListId)
        {
            var moviePlayList = GetMoviePlayListsByPlayListId(playListId);
          
            foreach(var movie in moviePlayList)
            {
                _context.MoviePlayLists.Remove(movie);
                _context.SaveChanges();
            }
            
        }

        public void DeleteMoviePlayListByIds(int playListId, int movieId)
        {
            var movieplaylist = _context.MoviePlayLists
                .FirstOrDefault(mpl => mpl.PlayListId == playListId && mpl.MovieId == movieId);

            if (movieplaylist != null)
            {
                _context.MoviePlayLists.Remove(movieplaylist);
                _context.SaveChanges();
            }
        }

        public void DeleteAllMoviePlayLists()
        {
            var moviePlayLists = _context.MoviePlayLists.ToList();
            _context.MoviePlayLists.RemoveRange(moviePlayLists);
            _context.SaveChanges();
        }
    }
}