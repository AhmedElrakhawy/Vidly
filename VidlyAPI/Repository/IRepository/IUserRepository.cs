using VidlyAPI.Models;

namespace VidlyAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUnique(string username);
        User Authanication(string username, string Passward, string Email);
        User Register(string username, string Passward, string Email);
    }
}
