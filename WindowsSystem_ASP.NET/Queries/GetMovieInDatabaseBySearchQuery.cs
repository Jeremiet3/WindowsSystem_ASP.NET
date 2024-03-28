using MediatR;
using WindowsSystem_ASP.NET.DAL.Entities;

namespace WindowsSystem_ASP.NET.Queries
{
    public class GetMovieInDatabaseBySearchQuery : IRequest<IEnumerable<Movie>>
    {
        public string SearchTerm { get; }

        public GetMovieInDatabaseBySearchQuery(string s)
        {
            SearchTerm = s;
        }
    }
}
