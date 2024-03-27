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

        public GetMoviesBySearchTermHandler(DataContext dbContext, TmdbApiService tmdbApiService)
        {
            _dbContext = dbContext;
            _tmdbApiService = tmdbApiService;
        }
        public async Task<IEnumerable<Movie>> Handle(GetMoviesBySearchTermQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await _tmdbApiService.SearchMoviesAsync(request.SearchTerm);
           
            if (searchResult == null || !searchResult.Any())
            {
                return Enumerable.Empty<Movie>();
            }

            var movies = new List<Movie>();

            foreach (var blMovie in searchResult)
            {
                // Check if the movie is in the database 
                var existingMovie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.TmdbId == blMovie.Id);

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

            return movies;
        }
    }
}
