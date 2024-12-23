using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace eTickets.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<Actor> Actors { get; set; }    
        public DbSet<Producer> Producers { get; set; }  
        public DbSet<Movie> Movies { get; set; }    
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId , 
                am.MovieId 
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(x => x.Movie).WithMany(x => x.Actors_Movies)
                .HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<Actor_Movie>().HasOne(x => x.Actor).WithMany(x => x.Actors_Movies)
                .HasForeignKey(x => x.ActorId);
        }
    }
}
