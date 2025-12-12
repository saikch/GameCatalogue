using Microsoft.EntityFrameworkCore;
using GamesCatalogue.Application.Entities;
using GamesCatalogue.Application.Interfaces;
using GamesCatalogue.Infrastructure.Data;

namespace GamesCatalogue.Infrastructure.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<VideoGame>> GetAllAsync()
        {
            return await _context.VideoGames
                .AsNoTracking()
                .OrderBy(v => v.Title)
                .ToListAsync();
        }

        public async Task<VideoGame?> GetByIdAsync(int id)
        {
            return await _context.VideoGames
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<VideoGame> CreateAsync(VideoGame game)
        {
            _context.VideoGames.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> UpdateAsync(VideoGame game)
        {
            _context.VideoGames.Update(game);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.VideoGames.FirstOrDefaultAsync(v => v.Id == id);
            if (existing == null) return false;

            _context.VideoGames.Remove(existing);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
