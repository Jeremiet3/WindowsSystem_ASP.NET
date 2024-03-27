using WindowsSystem_ASP.NET.DAL.Entities;

namespace WindowsSystem_ASP.NET.BL
{
    public class BlMovie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<BlGenre> Genres { get; set; } = new List<BlGenre>();
        public string? Release_Date { get; set; }
        public int RunTime { get; set; }
        public string? Overview { get; set; }
        public string? Poster_Path { get; set; }
        public string? PosterURL { get; set; }
        public string? TrailerURL { get; set; }
    }
}
