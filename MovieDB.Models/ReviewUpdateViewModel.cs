using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDB.Models
{
    public class ReviewUpdateViewModel
    {
        public int movieID { get; set; }

        public int ReviewId { get; set; }
        public string MovieName { get; set; }

        public int Star { get; set; }
        public string? Comment { get; set; }

    }
}
