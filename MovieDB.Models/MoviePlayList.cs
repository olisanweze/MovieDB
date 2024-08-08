using MovieDB.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDB.Models
{
    public class MoviePlayList
    {
        public int PlayListId { get; set; }
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Movie name is required.")]
        [StringLength(200, ErrorMessage = "Movie name cannot be longer than 200 characters.")]
        public string MovieName { get; set; }

        [ForeignKey("PlayListId")]
        public PlayList PlayList { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}

