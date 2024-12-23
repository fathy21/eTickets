﻿using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }
        [Display(Name = "Profile Picture ")]
        public string ProfilePictureUrl { get; set; }
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie> { };
    }
}
