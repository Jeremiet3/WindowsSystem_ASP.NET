using MediatR;
using WindowsSystem_ASP.NET.Commands;
using WindowsSystem_ASP.NET.DAL.Models;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class UpdateCommandHanlder : IRequestHandler<UpdateMovieCommand, int>
    {
        private readonly DataContext _dbContext;

        public UpdateCommandHanlder(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _dbContext.Movies.FindAsync(request.Id);
            if (movie == null)
            {
                return 0; // movue nod found
            }

            movie.Id = request.Id;
            movie.Title = request.Title ?? movie.Title;
            movie.PosterURL = request.PosterURL ?? movie.PosterURL;
            movie.Overview = request.Overview ?? movie.Overview;
            movie.TrailerURL= request.TrailerURL ?? movie.TrailerURL;
            movie.Genres = request.Genres ?? movie.Genres;
            movie.ReleaseDate = request.ReleaseDate ?? movie.ReleaseDate;
            movie.RunTime = request.RunTime ?? movie.RunTime;
            movie.TmdbId = request.TmdbId;
            await _dbContext.SaveChangesAsync();

            return 1; // Mise à jour réussie
        }
    }
}

