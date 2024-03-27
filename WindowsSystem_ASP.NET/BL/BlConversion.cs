using Newtonsoft.Json;
using WindowsSystem_ASP.NET.DAL.Entities;

namespace WindowsSystem_ASP.NET.BL
{
    public class BlConversion
    {
        private const string BaseImageUrl = "https://image.tmdb.org/t/p/";
        public static Movie? GetMovie(BlMovie bl_movie)
        {
            return new Movie()
            {
                Genres = bl_movie.Genres.Select(g => g.Name).ToList(),
                Overview = bl_movie.Overview,
                TmdbId = bl_movie.Id,
                PosterURL = GetPosterUrl(bl_movie.Poster_Path),
                ReleaseDate = bl_movie.Release_Date,
                RunTime = bl_movie.RunTime,
                Title = bl_movie.Title,
                TrailerURL = bl_movie.TrailerURL,
            };
        }

        public static string GetPosterUrl(string posterPath, string size = "w500")
        {
            if (string.IsNullOrWhiteSpace(posterPath))
            {
                return null; // Or return a default image URL
            }

            return $"{BaseImageUrl}{size}{posterPath}";
        }



    }
}
