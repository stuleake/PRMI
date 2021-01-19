using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnitTestDemo;
using UnitTestDemo.Models;

namespace UnitTestProject.Repos
{
    [TestFixture]
    public class GetByIdTests : QueryRepositoryBase
    {
        private BikeDbContext bikeDbContext;

        [Test]
        public void GetByIdReturnsModel()
        {
            // arrange
            var bikeId = Guid.NewGuid();
            var expected = new Bike { Id = bikeId, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            this.bikeDbContext = new InMemoryDbContextFactory<Bike>(new List<Bike> { expected }, true).GetBikeDbContext();
            var sut = CreateSut(this.bikeDbContext);

            // act
            var actual = sut.GetById(bikeId);

            //assert
            actual.Should().NotBeNull().And.BeEquivalentTo(expected);
        }

        [Test]
        public void GetByIdReturnsNull()
        {
            var bikeId = Guid.NewGuid();
            var expected = new Bike { Id = Guid.NewGuid(), Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            this.bikeDbContext = new InMemoryDbContextFactory<Bike>(new List<Bike> { expected }, true).GetBikeDbContext();
            var sut = CreateSut(this.bikeDbContext);

            // act
            var actual = sut.GetById(bikeId);

            //assert
            actual.Should().BeNull();
        }
    }
}
