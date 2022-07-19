using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VidlyAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Genre Genre { get; set; }

        [DisplayName("Genre")]
        [Required]
        public int GenreId { get; set; }
        [Required]
        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [DisplayName("Date Added")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        [DisplayName("Number In Stock")]
        public int? NumberInStock { get; set; }
    }
}
