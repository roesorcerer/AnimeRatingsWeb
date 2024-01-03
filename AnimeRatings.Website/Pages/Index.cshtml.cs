using AnimeRatings.Website.Models;
using AnimeRatings.Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnimeRatings.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileAnimesService animesService)
        {
            _logger = logger;
            AnimesService = animesService;

        }        
        public JsonFileAnimesService AnimesService { get; }
        public IEnumerable<Anime> Animes { get; private set; }

        public void OnGet() => Animes = AnimesService.GetAnimes();

    }
}
