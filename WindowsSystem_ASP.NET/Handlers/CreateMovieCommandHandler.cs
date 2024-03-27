using MediatR;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.BL;
using WindowsSystem_ASP.NET.Commands;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Services;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly DataContext _dbContext;
        private readonly TmdbApiService _tmdbApiService;

        public CreateMovieCommandHandler(DataContext dbContext,TmdbApiService tmdbApiService)
        {
            _dbContext = dbContext;
            _tmdbApiService = tmdbApiService;
        }
        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var _movie = _dbContext.Movies.FirstOrDefault(x => x.TmdbId == request.Id);
            if (_movie == null)
            {
                var movie  = await  _tmdbApiService.GetMovieByIdAsync(request.Id);
                _movie = BlConversion.GetMovie(movie);
                _dbContext.Movies.Add(_movie);
                await _dbContext.SaveChangesAsync();
            }
            return _movie;

        }
    }
}
