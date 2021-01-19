using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTestDemo.Converters;
using UnitTestDemo.Dto;
using UnitTestDemo.Models;
using UnitTestDemo.Repos;

namespace UnitTestDemo.Services
{
    public class BikeService : IBikeService
    {
        private readonly IQueryRepository<Bike> queryRepository;
        private readonly IBikeConverter converter;

        public BikeService(IQueryRepository<Bike> queryRepository, IBikeConverter converter)
        {
            this.queryRepository = queryRepository;
            this.converter = converter;
        }

        public async Task<List<BikeDto>> GetAsync()
        {
            var models = await this.queryRepository.GetAsync();

            return this.converter.ToDtoList(models).ToList();
        }

        public BikeDto GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} with a value of '{Guid.Empty}' is not allowed");
            }

            return this.converter.ToDto(this.queryRepository.GetById(id));
        }

        public async Task<List<BikeDto>> GetByWhere(Expression<Func<Bike, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} is null");
            }

            var models = await this.queryRepository.GetByWhere(predicate);

            return this.converter.ToDtoList(models).ToList();
        }

        public async Task<BikeDto> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException($"{nameof(name)} is null, empty or whitespace");
            }

            var model = await this.queryRepository.GetByName(name);

            return this.converter.ToDto(model);
        }
    }
}
