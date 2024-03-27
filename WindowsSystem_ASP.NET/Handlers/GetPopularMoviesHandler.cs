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
            var movies = new List<Movie>();

            foreach (var blMovie in searchResult)
            {
                // Vérifier si le film existe déjà dans la base de données par TmdbId
                var existingMovie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.TmdbId == blMovie.Id);

                if (existingMovie != null)
                {
                    // Si le film existe déjà, utilisez la version de la base de données
                    movies.Add(existingMovie);
                }
                else
                {
                    // Sinon, convertissez le BlMovie en Movie et ajoutez-le à la liste
                    var movie = BlConversion.GetMovie(blMovie);
                    movies.Add(movie);
                }
            }

            return movies;
        }
    }
}
