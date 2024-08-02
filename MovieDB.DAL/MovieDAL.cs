using MovieDB.Models;

namespace MovieDB.DAL
{
    public class MovieDAL
    {
        private readonly MovieDBContext _context;

        public MovieDAL(MovieDBContext context)
        {
            _context = context;
        }

        public List<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies.Find(id);
        }

        public async Task AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
