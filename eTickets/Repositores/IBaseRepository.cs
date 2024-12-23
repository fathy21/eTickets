namespace eTickets.Repositores
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T t);
        Task<T> Update(int id , T t);
        Task<T> Delete(int id);    

    }
}
