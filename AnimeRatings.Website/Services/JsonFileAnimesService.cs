using System.Text.Json;
using AnimeRatings.Website.Models;
using Microsoft.AspNetCore.Hosting;

namespace AnimeRatings.Website.Services
{
    public class JsonFileAnimesService
    {
        //taking the Json file information and passing to the webhost
        public JsonFileAnimesService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        //
        public IWebHostEnvironment WebHostEnvironment { get; }

        //search for the animes database file with webhost in wwwroot
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "animes.json"); }
        }

        //using IEnumerable for this instead of a list
        public IEnumerable<Anime> GetAnimes()
        {
            try
            {
                using (var jsonFileReader = File.OpenText(JsonFileName))
                {
                    return JsonSerializer.Deserialize<Anime[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here or rethrow it to be handled at a higher level
                throw new Exception("Error occurred while reading the JSON file.", ex);
            }
        }

        public void AddRating(string animeId, int rating)
        {
            var animes = GetAnimes();
            //quering the first animeId matches animeId
            //var query =animes.First(x => x.Id == animeId);
            //if query returns null 
            if (animes.First(x => x.Id == animeId).Ratings == null)
            {
                animes.First(x => x.Id == animeId).Ratings = new int[] { rating };
            }
            //else add query to list
            else
            {
                var ratings = animes.First(x => x.Id == animeId).Ratings?.ToList();
                ratings?.Add(rating);
                animes.First(x => x.Id == animeId).Ratings = ratings?.ToArray();
            }

            try
            {
                using (var outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<Anime>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        animes
                    );
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here or rethrow it to be handled at a higher level
                throw new Exception("Error occurred while writing to the JSON file.", ex);
            }
        }
    }
}


