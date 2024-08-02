using MovieDB.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieDB.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Movie title is required.")]
        [StringLength(200, ErrorMessage = "Movie title cannot be longer than 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2024, ErrorMessage = "Year must be between 1900 and 2024.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(200, ErrorMessage = "Genre cannot be longer than 200 characters.")]
        public string Genre { get; set; }

        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
        public double Rating { get; set; }

        [StringLength(200, ErrorMessage = "Director name cannot be longer than 200 characters.")]
        public string? Director { get; set; }

        [StringLength(200, ErrorMessage = "Actor cannot be longer than 200 characters.")]
        public string? Actors { get; set; }

        [StringLength(2000, ErrorMessage = "Plot cannot be longer than 2000 characters.")]
        public string? Plot { get; set; }

        [Url(ErrorMessage = "Invalid poster URL.")]
        public string? Poster { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
