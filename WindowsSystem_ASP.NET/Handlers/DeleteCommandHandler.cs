using MediatR;
using WindowsSystem_ASP.NET.Commands;
using WindowsSystem_ASP.NET.DAL.Models;

namespace WindowsSystem_ASP.NET.Handlers
{
    public class DeleteCommandHandler : IRequestHandler<DeleteMovieCommand, int>
    {
        private readonly DataContext _dbContext;

        public DeleteCommandHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _dbContext.Movies.FindAsync(request.Id);
            if (movie == null)
            {
                return 0; 
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return 1; 
        }
    }
}
