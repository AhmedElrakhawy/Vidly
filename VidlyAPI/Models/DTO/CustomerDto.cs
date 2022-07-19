using System;
using System.ComponentModel.DataAnnotations;

namespace VidlyAPI.Models.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer`s Name")]
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public byte MemberShipTypeId { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? BirthDay { get; set; }
    }
}
