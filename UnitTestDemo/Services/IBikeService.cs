using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTestDemo.Dto;
using UnitTestDemo.Models;

namespace UnitTestDemo.Services
{
    public interface IBikeService
    {
        Task<List<BikeDto>> GetAsync();

        BikeDto GetById(Guid id);

        Task<List<BikeDto>> GetByWhere(Expression<Func<Bike, bool>> predicate);

        Task<BikeDto> GetByName(string name);
    }
}
