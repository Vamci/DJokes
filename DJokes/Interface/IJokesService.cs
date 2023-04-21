using Microsoft.AspNetCore.Mvc;

namespace DJokes.Interface
{
    public interface IJokesService
    {
        public Task<ActionResult<string>> GetRandomJokeAsync();

        public Task<ActionResult<string>> GetJokeCount(int jokesCount);
    }
}
