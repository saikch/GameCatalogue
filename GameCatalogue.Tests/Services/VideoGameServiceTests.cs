using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GamesCatalogue.Application.DTOs;
using GamesCatalogue.Application.Entities;
using GamesCatalogue.Application.Interfaces;
using GamesCatalogue.Application.Services;
using Moq;
using Xunit;

namespace GamesCatalogue.Tests.Services
{
    public class VideoGameServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Returns_Games_Ordered_By_Title()
        {
            var repo = new Mock<IVideoGameRepository>();

            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<VideoGame>
            {
                new VideoGame
                {
                    Id = 2,
                    Title = "Zelda",
                    Platform = "Switch",
                    Genre = "Adventure",
                    ReleaseDate = new DateTime(2017, 3, 3),
                    Price = 69.99m,
                    IsAvailable = true
                },
                new VideoGame
                {
                    Id = 1,
                    Title = "Astro",
                    Platform = "PS5",
                    Genre = "Platformer",
                    ReleaseDate = new DateTime(2020, 11, 12),
                    Price = 59.99m,
                    IsAvailable = true
                }
            });

            var service = new VideoGameService(repo.Object);

            var result = await service.GetAllAsync();

            result.Select(x => x.Title)
                  .Should()
                  .ContainInOrder("Astro", "Zelda");
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Null_When_Game_Not_Found()
        {
            var repo = new Mock<IVideoGameRepository>();
            repo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((VideoGame?)null);

            var service = new VideoGameService(repo.Object);

            var result = await service.GetByIdAsync(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_Trims_Values_And_Returns_Created_Game()
        {
            var repo = new Mock<IVideoGameRepository>();

            repo.Setup(r => r.CreateAsync(It.IsAny<VideoGame>()))
                .ReturnsAsync((VideoGame g) =>
                {
                    g.Id = 1;
                    return g;
                });

            var service = new VideoGameService(repo.Object);

            var dto = new CreateVideoGameDto
            {
                Title = "  Hades  ",
                Platform = "  Switch ",
                Genre = " Action ",
                ReleaseDate = new DateTime(2020, 9, 17),
                Price = 24.99m,
                IsAvailable = true
            };

            var result = await service.CreateAsync(dto);

            result.Id.Should().Be(1);
            result.Title.Should().Be("Hades");
            result.Platform.Should().Be("Switch");
            result.Genre.Should().Be("Action");
            result.ReleaseDate.Should().Be(new DateTime(2020, 9, 17));
            result.Price.Should().Be(24.99m);
            result.IsAvailable.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_Returns_False_When_Game_Does_Not_Exist()
        {
            var repo = new Mock<IVideoGameRepository>();
            repo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((VideoGame?)null);

            var service = new VideoGameService(repo.Object);

            var dto = new UpdateVideoGameDto
            {
                Title = "Updated",
                Platform = "PC",
                Genre = "RPG",
                ReleaseDate = new DateTime(2015, 5, 19),
                Price = 49.99m,
                IsAvailable = false
            };

            var result = await service.UpdateAsync(1, dto);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_Calls_Repository()
        {
            var repo = new Mock<IVideoGameRepository>();
            repo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var service = new VideoGameService(repo.Object);

            var result = await service.DeleteAsync(1);

            result.Should().BeTrue();
        }
    }
}
