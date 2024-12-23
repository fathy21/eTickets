using eTickets.Models;
using eTickets.ViewModels;

namespace eTickets.Repositores
{
    public interface IMoveRepository : IBaseRepository<Movie>
    {
        Task <IEnumerable<Movie>> GetAllWithCinema();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
    
}
