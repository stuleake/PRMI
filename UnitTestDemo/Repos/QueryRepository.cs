using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTestDemo.Models;

namespace UnitTestDemo.Repos
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseModel
    {
        private readonly BikeDbContext bikeDbContext;

        public QueryRepository(BikeDbContext bikeDbContext)
        {
            this.bikeDbContext = bikeDbContext;
        }

        public Task<List<T>> GetAsync()
        {
            return this.bikeDbContext.Set<T>().ToListAsync();
        }

        public T GetById(Guid id)
        {
            return this.bikeDbContext.Set<T>().Find(id);
        }

        public Task<T> GetByName(string name)
        {
            return this.bikeDbContext.Set<T>().FindAsync(name).AsTask();
        }

        public async Task<List<T>> GetByWhere(Expression<Func<T, bool>> predicate)
        {
            return await this.bikeDbContext.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
