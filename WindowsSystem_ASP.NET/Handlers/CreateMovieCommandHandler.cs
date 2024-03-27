using MediatR;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.Commands;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly DataContext _dbContext;

        public CreateMovieCommandHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie()
            {
                Genres = request.Genres,
                Overview = request.Overview,
                PosterURL = request.PosterURL,
                ReleaseDate = request.ReleaseDate,
                RunTime = request.RunTime,
                Title = request.Title,
                TmdbId = request.TmdbId,
                TrailerURL = request.TrailerURL,
            };
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return movie;

        }
    }
}
