using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VidlyAPI.Data;
using VidlyAPI.Models;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(IOptions<AppSettings> appSettings, ApplicationDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }
        public User Authanication(string username, string Passward, string Email)
        {
            var UserObj = _dbContext.Users.SingleOrDefault(x => x.UserName == username && x.Password == Passward);
            if (UserObj == null)
                return null;

            var Tokenhandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var Discriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserObj.Id.ToString()),
                    new Claim(ClaimTypes.Role,UserObj.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var Token = Tokenhandler.CreateToken(Discriptor);
            UserObj.Token = Tokenhandler.WriteToken(Token);
            return UserObj;
        }

        public bool IsUnique(string username)
        {
            var User = _dbContext.Users.SingleOrDefault(x => x.UserName == username);
            if (User == null)
                return false;

            return true;
        }

        public User Register(string username, string Passward, string Email)
        {
            var User = new User
            {
                UserName = username,
                Password = Passward,
                Email = Email,
                Role = "Admin"
            };
            _dbContext.Users.Add(User);
            _dbContext.SaveChanges();
            User.Password = "";
            return User;
        }
    }
}
