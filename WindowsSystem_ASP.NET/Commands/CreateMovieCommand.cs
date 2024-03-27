using MediatR;
using WindowsSystem_ASP.NET.DAL.Entities;

namespace WindowsSystem_ASP.NET.Commands
{
    public class CreateMovieCommand : IRequest<Movie>
    {
        public int Id { get; set; }

        public CreateMovieCommand(int id)
        {
            Id = id;
        }


        //private int? runTime;

        //public string? Title { get; set; }
        //public List<string> Genres { get; set; } = new List<string>();
        //public string? ReleaseDate { get; set; }
        //public int RunTime { get; set; }
        //public string? Overview { get; set; }
        //public string? PosterURL { get; set; }
        //public string? TrailerURL { get; set; }
        //public int TmdbId { get; set; }

        //public CreateMovieCommand(string? title, List<string> genres, string? releaseDate, int runTime, string? overview, string? posterURL, string? trailerURL, int tmdbId)
        //{
        //    Title = title;
        //    Genres = genres;
        //    ReleaseDate = releaseDate;
        //    RunTime = runTime;
        //    Overview = overview;
        //    PosterURL = posterURL;
        //    TrailerURL = trailerURL;
        //    TmdbId = tmdbId;
        //}

    }
}
