using MediatR;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.BL;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Queries;
using WindowsSystem_ASP.NET.Services;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class GetMoviesBySearchTermHandler : IRequestHandler<GetMoviesBySearchTermQuery, IEnumerable<Movie>>
    {
        private readonly DataContext _dbContext;
        private readonly TmdbApiService _tmdbApiService;
        private readonly ImaggaApiService _maggaApiService;

        public GetMoviesBySearchTermHandler(DataContext dbContext, TmdbApiService tmdbApiService, ImaggaApiService maggaApiService)
        {
            _dbContext = dbContext;
            _tmdbApiService = tmdbApiService;
            _maggaApiService = maggaApiService;
        }
        public async Task<IEnumerable<Movie>> Handle(GetMoviesBySearchTermQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await _tmdbApiService.SearchMoviesAsync(request.SearchTerm);
           
            if (searchResult == null || !searchResult.Any())
            {
                return Enumerable.Empty<Movie>();
            }

            var movies = new List<Movie>();

            var allMovies = await _dbContext.Movies.ToListAsync(cancellationToken);

            foreach (var blMovie in searchResult)
            {
                // Check if the movie is in the database 
                var existingMovie = allMovies.FirstOrDefault(x => x.TmdbId == blMovie.Id);

                if (existingMovie != null)
                {
                    // if yes add the movie version of the database 
                    movies.Add(existingMovie);
                }
                else
                {
                    // else add the movie from the api after convert it from BlMovie to Movie
                    var movie = BlConversion.GetMovie(blMovie);
                    movies.Add(movie);
                }
            }

            // Imagga use 

            foreach (var movie in allMovies)
            {
                // check if the poster isn't null or empty 
                if (!string.IsNullOrEmpty(movie.PosterURL) && !searchResult.Any(m => m.Id == movie.TmdbId))
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
