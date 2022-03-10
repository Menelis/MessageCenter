using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Gateways.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task<T> GetById(object id);
        Task<T> Get(Expression<Func<T, bool>> condition);         
    }
}