using System;
using System.Threading.Tasks;
using FluentAssertions;
using GamesCatalogue.Api.Controllers;
using GamesCatalogue.Application.DTOs;
using GamesCatalogue.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GamesCatalogue.Tests.Controllers
{
    public class VideoGamesControllerTests
    {
        // GET:
        [Fact]
        public async Task GetById_Returns_NotFound_When_Game_Does_Not_Exist()
        {
            var service = new Mock<IVideoGameService>();
            service.Setup(s => s.GetByIdAsync(1))
                   .ReturnsAsync((VideoGameDto?)null);

            var controller = new VideoGamesController(service.Object);

            var result = await controller.GetById(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        // POST:
        [Fact]
        public async Task Create_Returns_CreatedAtAction_With_Created_Dto()
        {
            var service = new Mock<IVideoGameService>();
            service.Setup(s => s.CreateAsync(It.IsAny<CreateVideoGameDto>()))
                   .ReturnsAsync(new VideoGameDto
                   {
                       Id = 10,
                       Title = "Hades",
                       Platform = "Switch",
                       Genre = "Action",
                       ReleaseDate = new DateTime(2020, 9, 17),
                       Price = 24.99m,
                       IsAvailable = true
                   });

            var controller = new VideoGamesController(service.Object);

            var dto = new CreateVideoGameDto
            {
                Title = "Hades",
                Platform = "Switch",
                Genre = "Action",
                ReleaseDate = new DateTime(2020, 9, 17),
                Price = 24.99m,
                IsAvailable = true
            };

            var result = await controller.Create(dto);

            var created = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            created.ActionName.Should().Be(nameof(VideoGamesController.GetById));
            created.RouteValues!["id"].Should().Be(10);

            var body = created.Value.Should().BeOfType<VideoGameDto>().Subject;
            body.Id.Should().Be(10);
            body.Title.Should().Be("Hades");
        }

        // PUT: 
        [Fact]
        public async Task Update_Returns_NotFound_When_Service_Returns_False()
        {
            var service = new Mock<IVideoGameService>();
            service.Setup(s => s.UpdateAsync(1, It.IsAny<UpdateVideoGameDto>()))
                   .ReturnsAsync(false);

            var controller = new VideoGamesController(service.Object);

            var dto = new UpdateVideoGameDto
            {
                Title = "Updated Title",
                Platform = "PC",
                Genre = "RPG",
                ReleaseDate = new DateTime(2015, 5, 19),
                Price = 49.99m,
                IsAvailable = false
            };

            var result = await controller.Update(1, dto);

            result.Should().BeOfType<NotFoundResult>();
        }

        // DELETE 
        [Fact]
        public async Task Delete_Returns_NoContent_When_Service_Returns_True()
        {
            var service = new Mock<IVideoGameService>();
            service.Setup(s => s.DeleteAsync(1))
                   .ReturnsAsync(true);

            var controller = new VideoGamesController(service.Object);

            var result = await controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}
