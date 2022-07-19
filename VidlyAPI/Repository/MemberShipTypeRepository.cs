using System.Collections.Generic;
using System.Linq;
using VidlyAPI.Data;
using VidlyAPI.Models;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Repository
{
    public class MemberShipTypeRepository : IMemberShipTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MemberShipTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateMemberShipeType(MemberShipType memberShipType)
        {
            _dbContext.MemberShipTypes.Add(memberShipType);
            return Save();
        }

        public IEnumerable<MemberShipType> GetAllMemberShipTypes()
        {
            return _dbContext.MemberShipTypes.ToList();
        }

        public MemberShipType GetMemberShipType(int id)
        {
            return _dbContext.MemberShipTypes.Where(X => X.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
