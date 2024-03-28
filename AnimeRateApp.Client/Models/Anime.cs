using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AnimeRateApp.Client.Models
{
    // Represents a single Anime entity with all its properties.
    public class Anime
    {
        // Unique identifier for the Anime.
        public string? Id { get; set; }

        // The studio that produced the Anime.
        public string? Studio { get; set; }

        // URL to the image representing the Anime.
        [JsonPropertyName("img")]
        public string? Image { get; set; }

        // URL to the Anime's webpage or source.
        [JsonPropertyName("Url")]
        public string? Url { get; set; }

        // The title of the Anime.
        [JsonPropertyName("Title")]
        public string? Title { get; set; }

        // A short description of the Anime.
        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        // A collection of integer ratings for the Anime.
        [JsonPropertyName("Ratings")]
        public List<int>? Ratings { get; set; } = new List<int>();

        // Converts the Anime object to its JSON representation as a string.
        public override string ToString() => JsonSerializer.Serialize<Anime>(this);
    }
}