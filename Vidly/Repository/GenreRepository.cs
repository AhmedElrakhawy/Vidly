using System.Net.Http;
using Vidly.Models;
using Vidly.Repository.IRepository;

namespace Vidly.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly IHttpClientFactory _ClientFactory;
        public GenreRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _ClientFactory = clientFactory;
        }
    }
}
