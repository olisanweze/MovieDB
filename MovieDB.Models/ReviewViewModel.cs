using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDB.Models
{
    public class ReviewViewModel
    {
        public int Star { get; set; }
        public string? Comment { get; set; }

        // This is to store the movie the user has selected
        //public Movie? Movie { get; set; }

        public int SelectedMovieId { get; set; } // Used as a reference to the movie the user has selected
        
        // This is to display the movies in the dropdown
        public SelectList Movies { get; set; }
    }
}
