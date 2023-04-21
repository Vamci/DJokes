namespace DJokes.Models
{
    public class Jokes
    {
        public string? _id { get; set; }
        public string? punchline { get; set; }

        public string? setup { get; set; }

        public string? type { get; set; }

        public Author? author { get; set; }

        public string? approved { get; set; }

        public string? date { get; set; }

        public string? NSFW { get; set; }

        public string? shareableLink { get; set; }
    }
}
