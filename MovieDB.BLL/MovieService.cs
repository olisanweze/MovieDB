using MovieDB.DAL;
using MovieDB.Models;

namespace MovieDB.BLL
{
    public class MovieService
    {
        private readonly MovieDAL _movieDAL;

        public MovieService(MovieDAL movieDAL)
        {
            _movieDAL = movieDAL;
        }

        public List<Movie> GetMovies()
        {
            return _movieDAL.GetMovies();
        }

        public Movie GetMovie(int id)
        {
            return _movieDAL.GetMovie(id);
        }

        public async Task AddMovie(Movie movie)
        {
            await _movieDAL.AddMovie(movie);
        }

        public async Task UpdateMovie(Movie movie)
        {
            await _movieDAL.UpdateMovie(movie);
        }

        public async Task DeleteMovie(int id)
        {
            await _movieDAL.DeleteMovie(id);
        }
    }
}
