using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using UnitTestDemo.Controllers;
using UnitTestDemo.Dto;
using UnitTestDemo.Services;

namespace UnitTestProject.Controllers
{
    [TestFixture]
    public class BikeControllerTests
    {
        private const string ExpectedColour = "test colour";
        private const string ExpectedModel = "test model";
        private const string ExpectedName = "test name";

        private Mock<IBikeService> mockBikeService;
        
        [SetUp]
        public void SetUp()
        {
            mockBikeService = new Mock<IBikeService>();
        }

        [Test]
        public void GetByIdReturnsBadRequestWhenModelIsNull()
        {
            // arrange
            var id = Guid.NewGuid();
            BikeDto bikeDto = null;

            this.mockBikeService.Setup(service => service.GetById(id))
                .Returns(bikeDto)
                .Verifiable();
            
            var sut = this.CreateSut();

            // act
            var actual = sut.GetById(id);

            // assert
            actual.Should().BeOfType(typeof(BadRequestResult));
            this.mockBikeService.Verify(service => service.GetById(id), Times.Once);
        }

        [Test]
        public void GetByIdAsyncReturnsOkResultWhenModelIsNotNull()
        {
            // arrange
            var id = Guid.NewGuid();
            this.mockBikeService.Setup(service => service.GetById(id))
                .Returns(GetBikeDto())
                .Verifiable();

            var sut = this.CreateSut();

            // act
            var actual = sut.GetById(id);

            // assert
            actual.Should().BeOfType(typeof(OkObjectResult)).And.NotBeNull();

            this.mockBikeService.Verify(service => service.GetById(id), Times.Once);
        }

        [Test]
        public void GetByIdAsyncReturnsDtoWhenModelIsNotNull()
        {
            // arrange
            var id = Guid.NewGuid();
            var bikeDto = GetBikeDto();

            this.mockBikeService.Setup(service => service.GetById(id))
                .Returns(bikeDto)
                .Verifiable();

            var sut = this.CreateSut();

            // act
            var actual = sut.GetById(id) as OkObjectResult;

            // assert
            actual.Value.Should().NotBeNull().And.BeOfType(typeof(BikeDto)).And.BeSameAs(bikeDto);
            this.mockBikeService.Verify(service => service.GetById(id), Times.Once);
        }

        private BikeController CreateSut()
        {
            return new BikeController(mockBikeService.Object);
        }

        private static BikeDto GetBikeDto()
        {
            return new BikeDto { Colour = ExpectedColour, Model = ExpectedModel, Name = ExpectedName };
        }
    }
}
