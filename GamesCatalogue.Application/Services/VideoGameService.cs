using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesCatalogue.Application.DTOs;
using GamesCatalogue.Application.Entities;
using GamesCatalogue.Application.Interfaces;

namespace GamesCatalogue.Application.Services
{
    public class VideoGameService : IVideoGameService
    {
        private readonly IVideoGameRepository _repo;

        public VideoGameService(IVideoGameRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<VideoGameDto>> GetAllAsync()
        {
            var games = await _repo.GetAllAsync();
            return games
                .OrderBy(g => g.Title)
                .Select(ToDto)
                .ToList();
        }

        public async Task<VideoGameDto?> GetByIdAsync(int id)
        {
            var game = await _repo.GetByIdAsync(id);
            return game == null ? null : ToDto(game);
        }

        public async Task<VideoGameDto> CreateAsync(CreateVideoGameDto dto)
        {
            var entity = new VideoGame
            {
                Title = dto.Title.Trim(),
                Platform = dto.Platform.Trim(),
                Genre = dto.Genre.Trim(),
                ReleaseDate = dto.ReleaseDate,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable
            };

            var created = await _repo.CreateAsync(entity);
            return ToDto(created);
        }

        public async Task<bool> UpdateAsync(int id, UpdateVideoGameDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = dto.Title.Trim();
            existing.Platform = dto.Platform.Trim();
            existing.Genre = dto.Genre.Trim();
            existing.ReleaseDate = dto.ReleaseDate;
            existing.Price = dto.Price;
            existing.IsAvailable = dto.IsAvailable;

            return await _repo.UpdateAsync(existing);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repo.DeleteAsync(id);
        }

        // Mapping
        private static VideoGameDto ToDto(VideoGame game)
        {
            return new VideoGameDto
            {
                Id = game.Id,
                Title = game.Title,
                Platform = game.Platform,
                Genre = game.Genre,
                ReleaseDate = game.ReleaseDate,
                Price = game.Price,
                IsAvailable = game.IsAvailable
            };
        }
    }
}
