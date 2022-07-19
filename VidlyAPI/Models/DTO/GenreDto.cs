using System.ComponentModel.DataAnnotations;

namespace VidlyAPI.Models.DTO
{
    public class GenreDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
