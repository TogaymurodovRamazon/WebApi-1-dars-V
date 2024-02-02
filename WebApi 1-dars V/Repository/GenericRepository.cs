using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi_1_dars_V.AppDBContext;
using WebApi_1_dars_V.IRepository;
using WebApi_1_dars_V.Model;

namespace WebApi_1_dars_V.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : User
    {
        private readonly UserDBContext dBContext;
        private readonly DbSet<T> dbSet;
        public GenericRepository(UserDBContext dBContext)
        {
            this.dBContext = dBContext;
            this.dbSet=dBContext.Set<T>();
        }
        public async ValueTask<T> CreateAsync(T entity) =>
            (await dBContext.AddAsync(entity)).Entity;

        public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await GetAsync(expression);
            if(entity == null)
               return false;
            dbSet.Remove(entity);
            return true;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = expression is null ? dbSet: dbSet.Where(expression);
            if(includes!=null)
                foreach(var all in includes)
                    if(!string.IsNullOrEmpty(all))
                        query=query.Include(all);
            return query;
        }

        public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null) =>
            await dbSet.Where(expression).FirstOrDefaultAsync();

        public async ValueTask SaveChangesAsync()  =>
            await dBContext.SaveChangesAsync();

        public T Update(T entity)  =>
            dbSet.Update(entity).Entity;
    }
}
