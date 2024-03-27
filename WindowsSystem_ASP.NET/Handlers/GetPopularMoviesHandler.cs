using MediatR;
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
      
            var movies = searchResult.Select(blMovie => BlConversion.GetMovie(blMovie)).ToList();

            return movies;
        }
    }
}
