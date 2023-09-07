using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VideoGame.Abstractions;
using VideoGame.Entities;

namespace VideoGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IRepository<Game> _gameRepository;
        public GamesController(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> Get(string filterGenre = "")
        {
            if (string.IsNullOrWhiteSpace(filterGenre))
            {
                return await _gameRepository.GetAllAsync(game => game.Genres);
            }
            else
            {
                return await _gameRepository.GetAllAsync(game => game.Genres, game => game.Genres.Any(genre => genre.Name == filterGenre));
            }            
        }

        [HttpGet("{id}")]
        public async Task<Game?> Get(int id)
        {
            return await _gameRepository.GetAllRelatedDataAsync(id, game => game.Genres);
        }

        [HttpPost]
        public async Task<Game> Post([FromBody] Game game)
        {
            await _gameRepository.AddAsync(game);
            return game;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Game game)
        {
            await _gameRepository.UpdateAsync(game); 
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _gameRepository.GetAsync(id);

            if (user == null)
            {
                return BadRequest("Игры нет в библиотеке");
            }

            await _gameRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
