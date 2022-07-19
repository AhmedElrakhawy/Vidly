using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer`s Name")]
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MemberShipType MemberShipType { get; set; }

        [DisplayName("MemberShip Type")]
        [Required]
        public byte MemberShipTypeId { get; set; }

        [DisplayName("Date Of Birth")]
        //[Min18YearsIfAMember]
        public DateTime? BirthDay { get; set; }
    }
}
