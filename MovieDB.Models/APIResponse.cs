namespace MovieDB.Models
{
    public class APIResponse
    {
        public bool success { get; set; }
        public List<Movie> response { get; set; }
    }
}
