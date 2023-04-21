using DJokes.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DJokes.Services
{
    public class JokesService : IJokesService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory = null;
        public JokesService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult<string>> GetRandomJokeAsync()
        {
           
            using HttpClient client = _httpClientFactory.CreateClient();
            var body = string.Empty;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration.GetValue<string>("JokesHeader:BaseUri")),
                Headers =
                    {

                        {"X-RapidAPI-Key", _configuration.GetValue<string>("JokesHeader:X-RapidAPI-Key") },
                        {"X-RapidAPI-Host", _configuration.GetValue<string>("JokesHeader:X-RapidAPI-Host") }
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    body = await response.Content.ReadAsStringAsync();

                }

            }
            return body;
        }

        public async Task<ActionResult<string>> GetJokeCount(int count)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            var body = string.Empty;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration.GetValue<string>("JokesHeader:JokesCountUri") + count),
                Headers =
                    {

                        {"X-RapidAPI-Key", _configuration.GetValue<string>("JokesHeader:X-RapidAPI-Key") },
                        {"X-RapidAPI-Host", _configuration.GetValue<string>("JokesHeader:X-RapidAPI-Host") }
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    body = await response.Content.ReadAsStringAsync();
                }
            }
            return body;
        }

    }
}
