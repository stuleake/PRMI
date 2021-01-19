using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTestDemo.Models;

namespace UnitTestDemo.Repos
{
    public interface IQueryRepository<T> where T : BaseModel
    {
        Task<List<T>> GetAsync();

        T GetById(Guid id);

        Task<List<T>> GetByWhere(Expression<Func<T, bool>> predicate);

        Task<T> GetByName(string name);
    }
}
