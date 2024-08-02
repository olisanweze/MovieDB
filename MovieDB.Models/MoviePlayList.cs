using MovieDB.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieDB.Models
{
    public class MoviePlayList
    {
        public int PlayListId { get; set; }
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Movie name is required.")]
        [StringLength(200, ErrorMessage = "Movie name cannot be longer than 200 characters.")]
        public string MovieName { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}

