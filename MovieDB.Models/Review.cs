using System.ComponentModel.DataAnnotations;

namespace MovieDB.Models
{
    public class Review
    {
        //public int UserId { get; set; }
        public int movieID { get; set; }

        public int ReviewId { get; set; }
        public string MovieName { get; set; }
        //public string UserName { get; set; }
        public int Star { get; set; }
        public string? Comment { get; set; }

        //public User User { get; set; }
        public Movie Movie { get; set; }

        //public ICollection<User> Users { get; set; }
        //public ICollection<Movie> Movies { get; set; }
    }
}