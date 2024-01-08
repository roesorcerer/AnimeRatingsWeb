using System.Text.Json;
using AnimeRatings.Website.Models;
using Microsoft.EntityFrameworkCore;
using AnimeRatings.Website.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AnimeRatings.Website.Services
{
    // This service class is responsible for data operations on Anime entities
    public class JsonFileAnimesService
    {
        private readonly ApplicationDbContext DbContext;

        // Constructor injecting the database context to be used within this service
        public JsonFileAnimesService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // Asynchronously add a rating to an anime identified by its ID
        public async Task AddRatingAsync(string animeId, int rating)
        {
            var anime = await DbContext.Animes.FindAsync(animeId);
            if (anime != null)
            {
                // Initialize Ratings if null and add the new rating
                anime.Ratings = anime.Ratings ?? new List<int>();
                anime.Ratings.Add(rating);

                // Persist changes to the database
                await DbContext.SaveChangesAsync();
            }
        }

        // Retrieve an anime by its ID asynchronously
        public async Task<Anime> GetAnimeByIdAsync(string animeId)
        {
            return await DbContext.Animes.FindAsync(animeId);
        }

        // Asynchronously add a review to an anime identified by its ID
        public async Task AddReviewToAnimeAsync(string animeId, int rating)
        {
            var anime = await DbContext.Animes.FindAsync(animeId);
            if (anime != null)
            {
                // Initialize Ratings if null and add the new review rating
                anime.Ratings = anime.Ratings ?? new List<int>();
                anime.Ratings.Add(rating);

                // Persist changes to the database
                await DbContext.SaveChangesAsync();
            }
        }

        // Asynchronously retrieve all animes from the database
        public async Task<List<Anime>> GetAnimesAsync()
        {
            return await DbContext.Animes.ToListAsync();
        }
    }
}
