using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using UnitTestDemo;
using UnitTestDemo.Models;

namespace UnitTestProject.Repos
{
    [TestFixture]
    public class GetAsyncTests : QueryRepositoryBase
    {
        private BikeDbContext bikeDbContext;

        [Test]
        public async Task GetAsyncReturnsList()
        {
            // arrange
            var expected = GetExpectedModels(10).ToList();

            this.bikeDbContext = new InMemoryDbContextFactory<Bike>(expected, true).GetBikeDbContext();
            var sut = CreateSut(this.bikeDbContext);

            // act
            var actual = await sut.GetAsync();

            // assert
            actual.Should().NotBeNull().And.BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetAsyncReturnsEmptyList()
        {
            // arrange
            this.bikeDbContext = new InMemoryDbContextFactory<Bike>(null, false).GetBikeDbContext();
            var sut = CreateSut(this.bikeDbContext);

            // act
            var actual = await sut.GetAsync();

            // assert
            actual.Should().NotBeNull().And.BeEmpty();
        }

        private static IReadOnlyList<Bike> GetExpectedModels(int range)
        {
            return ImmutableList.CreateRange(Enumerable.Range(1, range).Select(index => new Bike()
            {
                Id = Guid.NewGuid(),
                Name = $"{ExpectedName}{index}",
                Colour = $"{ExpectedColour}{index}",
                Model = $"{ExpectedModel}{index}",
            }));
        }
    }
}
