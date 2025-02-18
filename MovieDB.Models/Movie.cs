using MovieDB.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieDB.Models
{
    public class Movie
    {
        public int movieID { get; set; }

        [Required(ErrorMessage = "Movie title is required.")]
        [StringLength(200, ErrorMessage = "Movie title cannot be longer than 200 characters.")]
        public string title { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2024, ErrorMessage = "Year must be between 1900 and 2024.")]
        public int year { get; set; }

     //   [Required(ErrorMessage = "Genre is required.")]
     //   [StringLength(200, ErrorMessage = "Genre cannot be longer than 200 characters.")]
     //   public string genre { get; set; }

        [StringLength(2000, ErrorMessage = "Plot cannot be longer than 2000 characters.")]
        public string? description { get; set; }

        [Url(ErrorMessage = "Invalid poster URL.")]
        public string? poster { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
