using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTestDemo.Converters;
using UnitTestDemo.Dto;
using UnitTestDemo.Models;
using UnitTestDemo.Repos;
using UnitTestDemo.Services;

namespace UnitTestProject.Services
{
    [TestFixture]
    public class BikeServiceTests
    {
        private Mock<IQueryRepository<Bike>> mockQueryRepository;
        private Mock<IBikeConverter> mockBikeConverter;

        private const string ExpectedColour = "test colour";
        private const string ExpectedModel = "test model";
        private const string ExpectedName = "test name";

        [SetUp]
        public void SetUp()
        {
            mockQueryRepository = new Mock<IQueryRepository<Bike>>();
            mockBikeConverter = new Mock<IBikeConverter>();
        }

        [Test]
        public async Task GetAsyncReturnsBikeDtoList()
        {
            // arrange
            var expected = new List<BikeDto>
            {
                new BikeDto { Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName },
                new BikeDto { Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName },
                new BikeDto { Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName }
            };

            mockBikeConverter.Setup(converter => converter.ToDtoList(It.IsAny<List<Bike>>()))
                .Returns(expected)
                .Verifiable();

            var sut = this.CreateSut();

            // act
            var actual = await sut.GetAsync();

            // assert
            actual.Should().BeEquivalentTo(expected);
            mockBikeConverter.Verify(converter => converter.ToDtoList(It.IsAny<List<Bike>>()), Times.Once);
        }

        [Test]
        public void GetByIdThrowsArgumentExceptionWhenIdIsNull()
        {
            // arrange
            var sut = this.CreateSut();

            // act & assert
            Assert.Throws<ArgumentException>(() => sut.GetById(Guid.Empty));
        }

        [Test]
        public void GetByIdReturnsDto()
        {
            // arrange
            var id = Guid.NewGuid();
            var model = new Bike { Id = id, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            var expected = new BikeDto { Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            
            this.mockQueryRepository.Setup(repo => repo.GetById(id))
                .Returns(model)
                .Verifiable();

            this.mockBikeConverter.Setup(converter => converter.ToDto(model))
                .Returns(expected)
                .Verifiable();

            var sut = this.CreateSut();

            // act
            var actual = sut.GetById(id);

            // assert
            actual.Should().BeEquivalentTo(expected);
            this.mockQueryRepository.Verify(repo => repo.GetById(id), Times.Once);
            this.mockBikeConverter.Verify(converter => converter.ToDto(model), Times.Once);
        }

        [Test]
        public void GetByWhereThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            // arrange
            var sut = this.CreateSut();
            Expression<Func<Bike, bool>> predicate = null;

            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetByWhere(predicate));
        }

        [Test]
        public async Task GetByWhereReturnsBikeDtoList()
        {
            // arrange
            var id = Guid.NewGuid();
            var models = new List<Bike>
            {
                new Bike { Id = id, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName },
                new Bike { Id = id, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName },
                new Bike { Id = id, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName }
            };

            var expected = new List<BikeDto>
            {
                new BikeDto { Colour = models[0].Colour, Model = models[0].Model, Name = models[0].Name },
                new BikeDto { Colour = models[1].Colour, Model = models[1].Model, Name = models[1].Name },
                new BikeDto { Colour = models[2].Colour, Model = models[2].Model, Name = models[2].Name },
            };

            this.mockQueryRepository.Setup(repo => repo.GetByWhere(It.IsAny<Expression<Func<Bike, bool>>>()))
                .Returns(Task.FromResult(models))
                .Verifiable();

            this.mockBikeConverter.Setup(converter => converter.ToDtoList(models))
                .Returns(expected)
                .Verifiable();

            var bike = new Bike { Id = id, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            Expression<Func<Bike, bool>> mockExpression = s => bike.Id == Guid.NewGuid();

            var sut = this.CreateSut();

            // act
            var actual = await sut.GetByWhere(mockExpression);

            // assert
            actual.Should().BeEquivalentTo(expected);
            this.mockQueryRepository.Verify(repo => repo.GetByWhere(mockExpression), Times.Once);
            this.mockBikeConverter.Verify(converter => converter.ToDtoList(models), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void GetByNameThrowsArgumentNullExceptionWhenNameIsNotValid(string name)
        {
            // arrange
            var sut = this.CreateSut();

            // act & assert
            Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetByName(name));
        }

        private IBikeService CreateSut()
        {
            return new BikeService(mockQueryRepository.Object, mockBikeConverter.Object);
        }
    }
}
