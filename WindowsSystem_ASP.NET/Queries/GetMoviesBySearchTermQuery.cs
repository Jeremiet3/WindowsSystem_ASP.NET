using MediatR;
using WindowsSystem_ASP.NET.DAL.Entities;

namespace WindowsSystem_ASP.NET.Queries
{
    public class GetMoviesBySearchTermQuery : IRequest<IEnumerable<Movie>>

    {
        public string SearchTerm { get;}  

        public GetMoviesBySearchTermQuery(string s)
        {
            SearchTerm = s;
        }
    }
}
