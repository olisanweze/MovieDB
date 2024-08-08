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

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Movie name is required.")]
        [StringLength(200, ErrorMessage = "Movie name cannot be longer than 200 characters.")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(200, ErrorMessage = "User name cannot be longer than 200 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Star rating is required.")]
        [Range(0, 5, ErrorMessage = "Star rating must be between 0 and 5.")]
        public int Star { get; set; }

        [StringLength(2000, ErrorMessage = "Comment cannot be longer than 2000 characters.")]
        public string? Comment { get; set; }
    }
}
