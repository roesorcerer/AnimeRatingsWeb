using AnimeRatings.Website.Models;
using AnimeRatings.Website.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRatings.Website.Controller
{
    // Attribute to specify that this class should be treated as a controller with an API interface.
    [ApiController]
    // Route template for this controller to handle HTTP requests.
    [Route("[controller]")]
    public class AnimesController : ControllerBase
    {
        // Constructor injecting the JsonFileAnimesService into the controller.
        public AnimesController(JsonFileAnimesService animeService) =>
            AnimeService = animeService;

        // Property to hold the service instance passed through dependency injection.
        public JsonFileAnimesService AnimeService { get; }

        // HTTP GET method to retrieve a collection of anime.
        // The method is asynchronous to allow non-blocking calls and database operations.
        [HttpGet]
        public async Task<IEnumerable<Anime>>? Get() => await AnimeService.GetAnimesAsync();

        // HTTP PATCH method to update a specific resource (anime rating in this case).
        // Accepts a RatingRequest object from the request body.
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            // Check if the incoming request has an AnimeId. If not, return a bad request response.
            if (request?.AnimeId == null)
                return BadRequest("Anime ID is required");

            // Call the service to add the rating asynchronously without waiting for the result.
            // The underscore is used to discard the result since it's not used here.
            _ = AnimeService.AddRatingAsync(request.AnimeId, request.Rating);
            return Ok();
        }

        // Nested class to model the data structure for a rating request.
        public class RatingRequest
        {
            // The ID of the anime to rate.
            public string? AnimeId { get; set; }
            // The rating value.
            public int Rating { get; set; }
        }
    }
}
