using eTickets.Models;

namespace eTickets.Repositores
{
    public interface IUnitOfWork : IDisposable  
    {
        IMoveRepository Movies { get; } 
        IBaseRepository<Actor> Actors { get; }  
        IBaseRepository<Producer> Producers { get; }    
        IBaseRepository<Cinema> Cinema { get; }
        IBaseRepository<Order> Orders { get; }  
        int Copmlete();
    }
}
