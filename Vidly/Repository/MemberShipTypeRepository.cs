using System.Net.Http;
using Vidly.Models;
using Vidly.Repository.IRepository;

namespace Vidly.Repository
{
    public class MemberShipTypeRepository : Repository<MemberShipType>, IMemberShipTypeRepository
    {
        private readonly IHttpClientFactory _ClientFactory;
        public MemberShipTypeRepository(IHttpClientFactory ClientFactory) : base(ClientFactory)
        {
            _ClientFactory = ClientFactory;
        }
    }
}
