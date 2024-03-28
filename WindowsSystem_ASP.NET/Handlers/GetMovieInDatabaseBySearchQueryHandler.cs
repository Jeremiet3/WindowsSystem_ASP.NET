using MediatR;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Queries;
using WindowsSystem_ASP.NET.Services;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class GetMovieInDatabaseBySearchQueryHandler : IRequestHandler<GetMovieInDatabaseBySearchQuery, IEnumerable<Movie>>
    {
        private readonly DataContext _dbContext;
        private readonly TmdbApiService _tmdbApiService;
        private readonly ImaggaApiService _maggaApiService;

        public GetMovieInDatabaseBySearchQueryHandler(DataContext dbContext, TmdbApiService tmdbApiService, ImaggaApiService maggaApiService)
        {
            _dbContext = dbContext;
            _tmdbApiService = tmdbApiService;
            _maggaApiService = maggaApiService;
        }
        public async Task<IEnumerable<Movie>> Handle(GetMovieInDatabaseBySearchQuery request, CancellationToken cancellationToken)
        {
            var allMovies = await _dbContext.Movies.ToListAsync(cancellationToken);

            var filteredMovies = allMovies.Where(movie =>
                                movie.Title != null &&
                                movie.Title.IndexOf(request.SearchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
            
            var movies = new List<Movie>();
            movies.AddRange(filteredMovies);

            foreach (var movie in allMovies)
            {
                // check if the poster isn't null or empty 
                if (!string.IsNullOrEmpty(movie.PosterURL) && !filteredMovies.Any(m => m.TmdbId == movie.TmdbId))
                {
                    // retrieve all tags from the API
                    var tags = await _maggaApiService.TagImageAsync(movie.PosterURL);

                    // check exist any tags that correspond to the searchTerm (key word) 
                    if (tags.Any(tag => tag.Contains(request.SearchTerm, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        // If yes add the movie to the return movies
                        movies.Add(movie);
                    }
                }
            }

            return movies;

        }
    }
}
