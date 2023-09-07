using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VideoGame.Abstractions;

namespace VideoGame.Repos
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _entity;
        public EFRepository(DbContext dbContext)
        {
            _entity = dbContext;
        }

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _entity.Set<T>().ToListAsync();
        //}

        public async Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, TProperty>>? include = null, Expression<Func<T, bool>>? where = null)
        {
            var query = _entity.Set<T>().AsQueryable();

            if (include != null)
            {
                query = query.Include(include);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            return await query.ToListAsync();
        }        

        public async Task<T?> GetAsync(int id)
        {
            return await _entity.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> GetAllRelatedDataAsync<TProperty>(int id, Expression<Func<T, TProperty>> include)
        {
            return await _entity.Set<T>()
                .Include(include)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _entity.Set<T>().AddAsync(entity);
            _entity.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            var entityOld = await _entity.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entity != null)
            {
                _entity.Entry(entityOld).CurrentValues.SetValues(entity);   
                await _entity.SaveChangesAsync();
            }              
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _entity.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _entity.Set<T>().Remove(entity);
                _entity.SaveChanges();
            }
        }         
    }
}
