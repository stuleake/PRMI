using UnitTestDemo;
using UnitTestDemo.Models;
using UnitTestDemo.Repos;

namespace UnitTestProject.Repos
{
    public class QueryRepositoryBase
    {
        public const string ExpectedColour = "test colour";
        public const string ExpectedModel = "test model";
        public const string ExpectedName = "test name";

        protected static IQueryRepository<Bike> CreateSut(BikeDbContext bikeDbContext)
        {
            return new QueryRepository<Bike>(bikeDbContext);
        }
    }
}
