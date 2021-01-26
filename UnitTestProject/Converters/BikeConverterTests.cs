using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            var models = GetExpectedModels(3).ToList();
            var expected = GetExpectedDtos(models);
            var sut = CreateSut();

            // act
            var actual = sut.ToDtoList(models);

            // assert
            actual.Should().BeEquivalentTo(expected);
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

        private static IReadOnlyList<BikeDto> GetExpectedDtos(IReadOnlyList<Bike> models)
        {
            return ImmutableList.CreateRange(Enumerable.Range(0, models.Count)
                .Select(index => GetBikeDto(models[index])));
        }

        private static BikeDto GetBikeDto(Bike model)
        {
            return new BikeDto { Colour = model.Colour, Model = model.Model, Name = model.Name };
        }

        private static IBikeConverter CreateSut()
        {
            return new BikeConverter();
        }
    }
}
