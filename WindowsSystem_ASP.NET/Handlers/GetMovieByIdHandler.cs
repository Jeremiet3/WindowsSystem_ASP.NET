using MediatR;
using WindowsSystem_ASP.NET.BL;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Queries;
using WindowsSystem_ASP.NET.Services;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly DataContext _dbContext;
        private readonly TmdbApiService _tmdbApiService;

        public GetMovieByIdHandler(DataContext dbContext, TmdbApiService tmdbApiService)
        {
            _dbContext = dbContext;
            _tmdbApiService = tmdbApiService;
        }
        public async Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _dbContext.Movies.FindAsync(request.Id);
            if (movie == null)
            {
                var blMovie = await _tmdbApiService.GetMovieByIdAsync(request.Id);
                if (blMovie != null)
                {
                    movie = BlConversion.GetMovie(blMovie);
                    return movie;
                }

                return null;
            }

            return movie;
        }
    }
}
