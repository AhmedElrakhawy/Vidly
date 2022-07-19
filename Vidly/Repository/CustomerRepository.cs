using System.Net.Http;
using Vidly.Models;
using Vidly.Repository.IRepository;

namespace Vidly.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomersRepository
    {
        private readonly IHttpClientFactory _ClientFactory;
        public CustomerRepository(IHttpClientFactory ClientFactory) : base(ClientFactory)
        {
            _ClientFactory = ClientFactory;
        }
    }
}
