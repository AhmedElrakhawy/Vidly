using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Repository.IRepository
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> Login(string Url, User User);
        Task<bool> Register(string Url, User User);
    }
}
