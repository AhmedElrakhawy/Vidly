using System.ComponentModel;

namespace VidlyAPI.Models.DTO
{
    public class MemberShipTypeDto
    {
        public byte Id { get; set; }
        [DisplayName("MemberShip Type")]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        [DisplayName("DisCount Rate")]
        public byte DiscountRate { get; set; }
        public static readonly byte UnKnown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}
