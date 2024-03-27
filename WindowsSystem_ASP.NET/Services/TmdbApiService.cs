using Newtonsoft.Json;
using System.Text.Json;
using WindowsSystem_ASP.NET.BL;
using WindowsSystem_ASP.NET.DAL.Entities;

namespace WindowsSystem_ASP.NET.Services
{
    public class TmdbApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "3b5dbe58cdabe7b146509c9f12153dbe"; 

        public TmdbApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BlMovie> GetMovieByIdAsync(int movieId)
        {
            var response = await _httpClient.GetAsync($"movie/{movieId}?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<BlMovie>(content);
                movie.TrailerURL = await GetTrailerUrlAsync(movie.Id);
              
                return movie;
            }

            return null; // Or handle the error as preferred
        }

        public async Task<IEnumerable<BlMovie>> SearchMoviesAsync(string query)
        {
            var response = await _httpClient.GetAsync($"search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(content);
                var results = jsonDoc.RootElement.GetProperty("results");
                var movieIds = results.EnumerateArray()
                                       .Where(result => result.GetProperty("original_language").GetString() == "en")
                                       .Select(result => result.GetProperty("id").GetInt32())
                                       .ToList();

                var tasks = movieIds.Select(id => GetMovieByIdAsync(id));
                var movies = await Task.WhenAll(tasks);

                return movies.Where(movie => movie != null); // Assuming GetMovieByIdAsync could return null, filter out any nulls
            }

            return Enumerable.Empty<BlMovie>();
        }

        public async Task<string> GetTrailerUrlAsync(int movieId)
        {
            var response = await _httpClient.GetAsync($"movie/{movieId}/videos?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(content);
                var results = jsonDoc.RootElement.GetProperty("results");
                foreach (var result in results.EnumerateArray())
                {
                    if (result.GetProperty("type").GetString() == "Trailer" && result.GetProperty("site").GetString() == "YouTube")
                    {
                        var youtubeKey = result.GetProperty("key").GetString();
                        return $"https://www.youtube.com/watch?v={youtubeKey}";
                    }
                }
            }
            return null; // Return null or a default value if no trailer is found
        }

        public async Task<IEnumerable<BlMovie>> GetPopularMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"movie/popular?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(content);
                var results = jsonDoc.RootElement.GetProperty("results");

                var movieIds = results.EnumerateArray()
                                      .Select(result => result.GetProperty("id").GetInt32())
                                      .ToList();

                var tasks = movieIds.Select(id => GetMovieByIdAsync(id)); // Assurez-vous que GetMovieByIdAsync retourne BlMovie
                var movies = await Task.WhenAll(tasks);

                return movies.Where(movie => movie != null);
            }

            return Enumerable.Empty<BlMovie>();
        }


    }
}
