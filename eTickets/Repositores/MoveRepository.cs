using eTickets.Data;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Repositores
{
    public class MoveRepository : BaseRepository<Movie> , IMoveRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MoveRepository(ApplicationDbContext dbContext) :base(dbContext)  
        {
            this.dbContext = dbContext;   
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Discription = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await dbContext.Movies.AddAsync(newMovie);
            await dbContext.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await dbContext.Actors_Movies.AddAsync(newActorMovie);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllWithCinema()
        {
            if (dbContext is null)
            {
                throw new InvalidOperationException("Dbcontext is not initialized");
            }
          return await dbContext.Movies.Include(x=>x.Cinema).OrderBy(x=>x.Name).ToListAsync(); 
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await dbContext.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await dbContext.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await dbContext.Cinema.OrderBy(n => n.Name).ToListAsync(),
                Producers = await dbContext.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await dbContext.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Discription = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageUrl = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await dbContext.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = dbContext.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            dbContext.Actors_Movies.RemoveRange(existingActorsDb);
            await dbContext.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await dbContext.Actors_Movies.AddAsync(newActorMovie);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
