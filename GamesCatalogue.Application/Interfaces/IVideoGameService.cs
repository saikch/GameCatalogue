using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalogue.Application.DTOs;

namespace GamesCatalogue.Application.Interfaces
{
    public interface IVideoGameService
    {
        Task<IReadOnlyList<VideoGameDto>> GetAllAsync();
        Task<VideoGameDto?> GetByIdAsync(int id);
        Task<VideoGameDto> CreateAsync(CreateVideoGameDto dto);
        Task<bool> UpdateAsync(int id, UpdateVideoGameDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
