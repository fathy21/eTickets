
using eTickets.Data;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Repositores
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> Add(T t)
        {
           var element =  await dbContext.Set<T>().AddAsync(t);
            await dbContext.SaveChangesAsync();
            return element.Entity;   
        }

        public async Task<T> Delete(int id)
        {
            var element = await dbContext.Set<T>().FindAsync(id);
            if(element == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            dbContext.Set<T>().Remove(element);
            await dbContext.SaveChangesAsync();
            return element;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(int id, T t)
        {
            var existingEntity = await dbContext.Set<T>().FindAsync(id);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            dbContext.Entry(existingEntity).CurrentValues.SetValues(t);
            await dbContext.SaveChangesAsync();

            return existingEntity;
        }

    }
}
