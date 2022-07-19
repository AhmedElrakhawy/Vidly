using System.Net.Http;
using Vidly.Models;
using Vidly.Repository.IRepository;

namespace Vidly.Repository
{
    public class MoviesRepository : Repository<Movie>, IMoviesRepository
    {
        readonly IHttpClientFactory _clientFactory;
        public MoviesRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
