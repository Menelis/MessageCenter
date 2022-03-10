using System.Linq;
using System;
using System.Linq.Expressions;
using Core.Gateways.Repositories;
using Infastructure.EF;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infastructure.Gateways.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        protected readonly MessageCenterDbContext _dbContext;
        private readonly DbSet<T> Table;

        protected RepositoryBase(MessageCenterDbContext dbContext)
        {
            _dbContext = dbContext;
            Table = _dbContext.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            Table.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(Expression<Func<T, bool>> condition)
        {
            IQueryable<T> query = Table.Where(condition);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await Table.FindAsync(id);
        }
    }
}