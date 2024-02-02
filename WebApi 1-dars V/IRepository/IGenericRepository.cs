using System.Linq.Expressions;
using WebApi_1_dars_V.Model;

namespace WebApi_1_dars_V.IRepository
{
    public interface IGenericRepository<T> where T : User
    {
        IQueryable<T> GetAll(Expression<Func<T,bool>> expression, string[] includes=null);
        ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
        ValueTask<T> CreateAsync(T entity);
        ValueTask<bool> DeleteAsync(Expression<Func<T,bool>>  expression);
        T Update(T entity);
        public ValueTask SaveChangesAsync();
    }
}
