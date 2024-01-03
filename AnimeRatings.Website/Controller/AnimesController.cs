using AnimeRatings.Website.Models;
using AnimeRatings.Website.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRatings.Website.Controller
{
    [ApiController]    
    [Route("[controller]")]

    public class AnimesController : ControllerBase
    {
        //using controller to call the JsonFileAnimeService I created as an argument
        public AnimesController(JsonFileAnimesService animeService) => 
            AnimeService = animeService;
        //holding the JsonFileAnimesService information to AnimeService
        public JsonFileAnimesService AnimeService { get; }

        [HttpGet]
        public IEnumerable<Anime> Get() => AnimeService.GetAnimes();        

        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request) 
        {
            if (request?.AnimeId == null)
                return BadRequest("Anime ID is required");

            AnimeService.AddRating(request.AnimeId, request.Rating);
            return Ok();
        }
        public class RatingRequest
        {
            public string? AnimeId { get; set; }
            public int Rating { get; set; }
        }
    }
}
