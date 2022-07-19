using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vidly.Models;
using Vidly.Repository.IRepository;

namespace Vidly.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly IHttpClientFactory _ClientFactory;
        public AccountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        public async Task<User> Login(string Url, User User)
        {
            var Request = new HttpRequestMessage(HttpMethod.Post, Url);
            if (User != null)
            {
                Request.Content = new StringContent(JsonConvert.SerializeObject(User)
                    , Encoding.UTF8
                    , "application/json");
            }
            else
            {
                return new User();
            }
            var Client = _ClientFactory.CreateClient();
            var Response = await Client.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                var stringJson = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(stringJson);
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> Register(string Url, User User)
        {
            var Request = new HttpRequestMessage(HttpMethod.Post, Url);
            if (User != null)
            {
                Request.Content = new StringContent(JsonConvert.SerializeObject(User),
                    Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }
            var Client = _ClientFactory.CreateClient();
            var Response = await Client.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
