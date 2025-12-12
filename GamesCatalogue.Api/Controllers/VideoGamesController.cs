using GamesCatalogue.Application.DTOs;
using GamesCatalogue.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GamesCatalogue.Api.Controllers
{
    [ApiController]
    [Route("api/v1/videogames")]
    public class VideoGamesController : ControllerBase
    {
        private readonly IVideoGameService _videoGameService;

        public VideoGamesController(IVideoGameService videoGameService)
        {
            _videoGameService = videoGameService;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VideoGameDto>>> GetAll()
        {
            var games = await _videoGameService.GetAllAsync();
            return Ok(games);
        }


        // GET:
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VideoGameDto>> GetById(int id)
        {
            var game = await _videoGameService.GetByIdAsync(id);
            if (game is null)
                return NotFound();

            return Ok(game);
        }


        // POST:
        [HttpPost]
        public async Task<ActionResult<VideoGameDto>> Create([FromBody] CreateVideoGameDto dto)
        {
            var created = await _videoGameService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT:
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVideoGameDto dto)
        {

            var updated = await _videoGameService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _videoGameService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
