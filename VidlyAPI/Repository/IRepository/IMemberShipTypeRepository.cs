using System.Collections.Generic;
using VidlyAPI.Models;

namespace VidlyAPI.Repository.IRepository
{
    public interface IMemberShipTypeRepository
    {
        MemberShipType GetMemberShipType(int id);
        IEnumerable<MemberShipType> GetAllMemberShipTypes();
        bool CreateMemberShipeType(MemberShipType memberShipType);
        bool Save();
    }
}
