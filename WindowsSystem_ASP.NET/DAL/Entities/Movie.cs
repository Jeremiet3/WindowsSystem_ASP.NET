namespace WindowsSystem_ASP.NET.DAL.Entities
{
    public class Movie
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

    }
}
