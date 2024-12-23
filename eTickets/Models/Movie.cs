using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Discription { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }    
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }   
        public MovieCategory MovieCategory { get; set; }
        public ICollection<Actor_Movie> Actors_Movies { get; set; } = new HashSet<Actor_Movie> { };
        public int CinemaId { get; set; }
        [ForeignKey(nameof(CinemaId))]
        public Cinema Cinema { get; set; }

        public int ProducerId { get; set; }
        [ForeignKey(nameof(ProducerId))]
        public Producer Producer { get; set; }
    }
}
