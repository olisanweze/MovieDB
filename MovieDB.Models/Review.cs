using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDB.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }  // Consider adding a primary key if you don't have one

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int movieID { get; set; }

        public int ReviewId { get; set; }
        public string MovieName { get; set; }
        public string UserName { get; set; }
        public int Star { get; set; }
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Star is required.")]
        [Range(0, 5, ErrorMessage = "Star must be between 0 and 5.")]
        public int Star { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
