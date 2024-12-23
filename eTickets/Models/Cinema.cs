using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema
    {
        public int Id { get; set; }

        [Display(Name ="Cinema Logo")]
        public string Logo { get; set; }
        [Display(Name = " Name")]
        public string Name { get; set; }
        [Display(Name = "Discription")]
        public string Discription { get; set; }
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie> { };
    }
}
