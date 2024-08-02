using MovieDB.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieDB.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(200, ErrorMessage = "User name cannot be longer than 200 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<MoviePlayList> MoviePlayLists { get; set; }
    }
}
