using MovieDB.DAL;
using MovieDB.Models;

namespace MovieDB.BLL
{
    public class UserService
    {
        private readonly UserDAL _userDAL;

        public UserService(UserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public List<User> GetUsers()
        {
            return _userDAL.GetUsers();
        }

        public User GetUser(int id)
        {
            return _userDAL.GetUser(id);
        }

        public void AddUser(User user)
        {
            _userDAL.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _userDAL.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userDAL.DeleteUser(id);
        }
    }
}
