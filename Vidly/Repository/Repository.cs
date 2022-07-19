using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vidly.Repository.IRepository;

namespace Vidly.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _Factory;
        public Repository(IHttpClientFactory factory)
        {
            _Factory = factory;
        }
        public async Task<bool> CreateAsync(string Url, T ObjToCreate)
        {
            var Request = new HttpRequestMessage(HttpMethod.Post, Url);
            if (ObjToCreate != null)
            {
                Request.Content = new StringContent(JsonConvert.SerializeObject(ObjToCreate),
                    Encoding.UTF8, "application/json");
            }
            var Client = _Factory.CreateClient();
            var Response = await Client.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string Url, int Id)
        {
            var Request = new HttpRequestMessage(HttpMethod.Delete, Url + Id);
            var Client = _Factory.CreateClient();
            var Response = await Client.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<IEnumerable<T>> GetAll(string Url)
        {
            var Request = new HttpRequestMessage(HttpMethod.Get, Url);
            var Client = _Factory.CreateClient();
            var Response = await Client.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                var JsonString = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(JsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetAsync(string Url, int Id)
        {
            var Request = new HttpRequestMessage(HttpMethod.Get, Url + Id);
            var Client = _Factory.CreateClient();
            var Response = await Client.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                var JsonString = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(JsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string Url, T ObjToUpdate)
        {
            var Request = new HttpRequestMessage(HttpMethod.Delete, Url);
            if (ObjToUpdate != null)
            {
                Request.Content = new StringContent(JsonConvert.SerializeObject(ObjToUpdate), Encoding.UTF8
                    , "application/json");
            }
            var CLient = _Factory.CreateClient();
            var Response = await CLient.SendAsync(Request);
            if (Response.StatusCode == HttpStatusCode.NoContent)
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
