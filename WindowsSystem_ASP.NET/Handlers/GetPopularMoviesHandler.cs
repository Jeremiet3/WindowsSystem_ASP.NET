using MediatR;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.BL;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Queries;
using WindowsSystem_ASP.NET.Services;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class GetPopularMoviesHandler : IRequestHandler<GetPopularMoviesQuery, IEnumerable<Movie>>
    {
        private readonly DataContext _dbContext;
        private readonly TmdbApiService _tmdbApiService;

        public GetPopularMoviesHandler(DataContext dbContext, TmdbApiService tmdbApiService)
        {
            _dbContext = dbContext;
            _tmdbApiService = tmdbApiService;
        }
        public async Task<IEnumerable<Movie>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await _tmdbApiService.GetPopularMoviesAsync();
            var getmovies = await _dbContext.Movies.ToListAsync();
            var movies = new List<Movie>();

            foreach (var blMovie in searchResult)
            {
                var existingMovie = getmovies.FirstOrDefault(x=>x.TmdbId == blMovie.Id);

                if (existingMovie != null)
                {
                    movies.Add(existingMovie);
                    getmovies.Remove(existingMovie);
                }
                else
                {
                    var movie = BlConversion.GetMovie(blMovie);
                    movies.Add(movie);
                }

            }
            movies.AddRange(getmovies);

            return movies;
        }
    }
}
