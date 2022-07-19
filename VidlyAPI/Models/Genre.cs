﻿using System.ComponentModel.DataAnnotations;

namespace VidlyAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
