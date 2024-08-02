using MovieDB.Models;

namespace MovieDB.DAL
{
    public class PlayListDAL
    {
        private readonly MovieDBContext _context;

        public PlayListDAL(MovieDBContext context)
        {
            _context = context;
        }

        public List<PlayList> GetPlayLists()
        {
            return _context.PlayLists.ToList();
        }

        public PlayList GetPlayList(int id)
        {
            return _context.PlayLists.Find(id);
        }

        public void AddPlayList(PlayList playlist)
        {
            _context.PlayLists.Add(playlist);
            _context.SaveChanges();
        }

        public void UpdatePlayList(PlayList playlist)
        {
            _context.PlayLists.Update(playlist);
            _context.SaveChanges();
        }

        public void DeletePlayList(int id)
        {
            var playlist = _context.PlayLists.Find(id);
            if (playlist != null)
            {
                _context.PlayLists.Remove(playlist);
                _context.SaveChanges();
            }
        }
    }
}
