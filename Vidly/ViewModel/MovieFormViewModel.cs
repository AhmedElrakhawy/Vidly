using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Genre")]
        [Required]
        public int GenreId { get; set; }
        [Required]
        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Number In Stock")]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }
        public string Title
        {
            get
            {
                if (Id != 0)
                    return "Edit Movie";
                return "New Movie";
            }
        }
        public MovieFormViewModel()
        {
            Id = 0;
        }
        public MovieFormViewModel(Movie Movie)
        {
            Id = Movie.Id;
            Name = Movie.Name;
            ReleaseDate = Movie.ReleaseDate;
            NumberInStock = Movie.NumberInStock;
            GenreId = Movie.GenreId;
        }
    }
}
