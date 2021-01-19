using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestDemo.Converters;
using UnitTestDemo.Dto;
using UnitTestDemo.Models;

namespace UnitTestProject.Converters
{
    [TestFixture]
    public class BikeConverterTests
    {
        private const string ExpectedColour = "test colour";
        private const string ExpectedModel = "test model";
        private const string ExpectedName = "test name";

        [Test]
        public void ToDtoReturnsEmptyDtoWhenModelIsNull()
        {
            // arrange
            var sut = CreateSut();

            // act
            var actual = sut.ToDto(null);

            // assert
            actual.Should().NotBeNull();
        }

        [Test]
        public void ToDtoReturnsDtoWhenModelIsNotNull()
        {
            // arrange
            var id = Guid.NewGuid();
            var model = new Bike { Id = id, Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            var expected = new BikeDto { Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
            var sut = CreateSut();

            // act
            var actual = sut.ToDto(model);

            // assert
            actual.Should().NotBeNull().And.BeEquivalentTo(expected);
        }

        [Test]
        public void ToDtoListReturnsEmptyListWhenModelsAreNull()
        {
            // arrange
            List<Bike> models = null;
            var sut = CreateSut();

            // act
            var actual = sut.ToDtoList(models);

            // assert
            actual.Should().NotBeNull().And.BeEmpty();
        }

        [Test]
        public void ToDtoListReturnsEmptyListWhenModelsAreEmpty()
        {
            // arrange
            var sut = CreateSut();

            // act
            var actual = sut.ToDtoList(Enumerable.Empty<Bike>().ToList());

            // assert
            actual.Should().NotBeNull().And.BeEmpty();
        }

        [Test]
        public void ToDtoListReturnsListWhenModelsAreNotNullOrEmpty()
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

            var sut = CreateSut();

            // act
            var actual = sut.ToDtoList(models);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }

        private static IBikeConverter CreateSut()
        {
            return new BikeConverter();
        }
    }
}
