using MediatR;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Services;

namespace WindowsSystem_ASP.NET.Queries
{
    public class GetPopularMoviesQuery : IRequest<IEnumerable<Movie>>
    {
       
    }
}
