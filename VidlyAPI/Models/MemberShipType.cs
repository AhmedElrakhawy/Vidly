using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VidlyAPI.Models
{
    public class MemberShipType
    {
        [Key]
        [Required]
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
