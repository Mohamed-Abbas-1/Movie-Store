﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please insert the movie name.")]
        [StringLength(255, ErrorMessage = "Name can't be more than 255 chars.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please insert the release date.")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateAdded { get; set; }

        [Required(ErrorMessage = "Please insert the Number in stock.")]
        [Range(1, 20, ErrorMessage = "The number in stock can not be less than 1 or more than 20.")]
        public byte? NumberInStock { get; set; }
        public byte? NumberAvailable { get; set; }

        [Required(ErrorMessage = "Please insert the movie genre.")]
        public byte GenreId { get; set; }
        public virtual GenerDto Genre { get; set; }
    }
}