using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UnitTestDemo;

namespace UnitTestProject.Repos
{
    public class InMemoryDbContextFactory<T> where T : class
    {
        private readonly BikeDbContext context;

        public InMemoryDbContextFactory(IEnumerable<T> data, bool seedData = true)
        {
            var options = new DbContextOptionsBuilder<BikeDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;

            context = new BikeDbContext(options);

            if (seedData == false) return;

            context.Set<T>().AddRange(data);
            context.SaveChanges();
        }

        public BikeDbContext GetBikeDbContext()
        {
            return context;
        }
    }
}
