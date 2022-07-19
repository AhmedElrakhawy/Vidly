using System.ComponentModel;

namespace Vidly.Models
{
    public class MemberShipType
    {
        public byte Id { get; set; }
        [DisplayName("MemberShip Type")]
        public string Name { get; set; }
        public byte DiscountRate { get; set; }
    }
}
