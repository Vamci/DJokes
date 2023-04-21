using DJokes.Interface;
using DJokes.Models;
using DJokes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace DJokes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JokesAPIController : ControllerBase
    {
        private IJokesService _jokeService;
        private readonly ILogger<JokesAPIController> _logger;
        public JokesAPIController(IJokesService jokeService, ILogger<JokesAPIController> logger)
        {

            this._jokeService = jokeService;
            _logger = logger;   
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomJokeAsync()
        {
            List<Jokes>? jokes = new List<Jokes>();

            try
            {
                dynamic resObject = await _jokeService.GetRandomJokeAsync();
                dynamic data = JsonConvert.DeserializeObject<resObject>(resObject.Value) ?? throw new ArgumentException();
                jokes = ((DJokes.Models.resObject)data).body;
                return Ok(jokes);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError(ex.Message);
                throw;

            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetJokeCount(int jokesCount)
        {

            try
            {
                List<Jokes>? jokes = new List<Jokes>();
                dynamic resObject = await _jokeService.GetJokeCount(jokesCount);
                dynamic data = JsonConvert.DeserializeObject<resObject>(resObject.Value) ?? throw new ArgumentException();
                jokes = ((DJokes.Models.resObject)data).body;
                return Ok(jokes);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError(ex.Message);
                throw;

            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
        }


    }
}
