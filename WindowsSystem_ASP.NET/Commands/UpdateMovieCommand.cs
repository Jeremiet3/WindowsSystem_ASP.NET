using MediatR;

namespace WindowsSystem_ASP.NET.Commands
{
    public class UpdateMovieCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public string? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public string? Overview { get; set; }
        public string? PosterURL { get; set; }
        public string? TrailerURL { get; set; }
        public int TmdbId { get; set; }

        public UpdateMovieCommand(int id, string? title, List<string> genres, string? releaseDate, int runTime, string? overview, string? posterURL, string? trailerURL, int tmdbId)
        {
            Id = id;
            Title = title;
            Genres = genres;
            ReleaseDate = releaseDate;
            RunTime = runTime;
            Overview = overview;
            PosterURL = posterURL;
            TrailerURL = trailerURL;
            TmdbId = tmdbId;
        }

        public UpdateMovieCommand(int id, string? title, List<string> genres, string? releaseDate, int? runTime, string? overview, string? posterURL, string? trailerURL, int tmdbId)
        {
            Id = id;
            Title = title;
            Genres = genres;
            ReleaseDate = releaseDate;
            RunTime = runTime;
            Overview = overview;
            PosterURL = posterURL;
            TrailerURL = trailerURL;
            TmdbId = tmdbId;
        }
    }
}
