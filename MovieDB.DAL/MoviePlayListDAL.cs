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

        public MoviePlayList GetMoviePlayList(int id)
        {
            return _context.MoviePlayLists.Find(id);
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

        public void DeleteMoviePlayList(int id)
        {
            var movieplaylist = _context.MoviePlayLists.Find(id);
            if (movieplaylist != null)
            {
                _context.MoviePlayLists.Remove(movieplaylist);
                _context.SaveChanges();
            }
        }
    }
}