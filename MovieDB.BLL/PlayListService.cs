using MovieDB.DAL;
using MovieDB.Models;

namespace MovieDB.BLL
{
    public class PlayListService
    {
        private readonly PlayListDAL _playlistDAL;

        public PlayListService(PlayListDAL playlistDAL)
        {
            _playlistDAL = playlistDAL;
        }

        public List<PlayList> GetPlayLists()
        {
            return _playlistDAL.GetPlayLists();
        }

        public PlayList GetPlayList(int id)
        {
            return _playlistDAL.GetPlayList(id);
        }

        public void AddPlayList(PlayList playlist)
        {
            _playlistDAL.AddPlayList(playlist);
        }

        public void UpdatePlayList(PlayList playlist)
        {
            _playlistDAL.UpdatePlayList(playlist);
        }

        public void DeletePlayList(int id)
        {
            _playlistDAL.DeletePlayList(id);
        }

        public void DeleteAllPlayLists()
        {
            _playlistDAL.DeleteAllPlayLists();
        }
    }
}
