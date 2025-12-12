using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalogue.Application.Entities;

namespace GamesCatalogue.Application.Interfaces
{
    public interface IVideoGameRepository
    {
        Task<IReadOnlyList<VideoGame>> GetAllAsync();
        Task<VideoGame?> GetByIdAsync(int id);
        Task<VideoGame> CreateAsync(VideoGame game);
        Task<bool> UpdateAsync(VideoGame game);
        Task<bool> DeleteAsync(int id);
    }
}
