using eTickets.Data;
using eTickets.Models;

namespace eTickets.Repositores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            Movies = new MoveRepository (dbContext);
            Actors = new BaseRepository<Actor>(dbContext);
            Producers = new BaseRepository<Producer>(dbContext);    
            Cinema = new BaseRepository<Cinema>(dbContext); 
            Orders = new BaseRepository<Order>(dbContext);   
        }

        public IMoveRepository Movies { get; private set; }

        public IBaseRepository<Actor> Actors { get; private set; }

        public IBaseRepository<Producer> Producers { get; private set; }

        public IBaseRepository<Cinema> Cinema { get; private set; }
        public IBaseRepository<Order> Orders { get; private set; }  

        public int Copmlete()
        {
            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
