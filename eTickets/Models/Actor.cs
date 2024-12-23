using System;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eTickets.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        [Required (ErrorMessage ="the Full Name is required")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "the Biography is required")]
        public string Bio { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "the Profile Picture is required")]
        public string ProfilePictureUrl { get; set; }
        public ICollection<Actor_Movie> Actors_Movies { get; set; } = new HashSet<Actor_Movie> { };
    }
}
