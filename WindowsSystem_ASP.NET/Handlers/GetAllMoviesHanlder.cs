using MediatR;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Queries;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class GetAllMoviesHanlder : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movie>>
    {
        private readonly DataContext _dbContext;

        public GetAllMoviesHanlder(DataContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            if (_dbContext == null)
            {
                return Enumerable.Empty<Movie>();
            }
            return await _dbContext.Movies.ToListAsync();
        }
    }
}
