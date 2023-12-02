using System.Threading.Tasks;
using tasiapi.Models;
namespace tasiapi.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user,string password);
       public User Login(string userName, string password);
        bool UserExists(string userName);
    }
}
