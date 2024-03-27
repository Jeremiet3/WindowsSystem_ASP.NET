using MediatR;

namespace WindowsSystem_ASP.NET.Commands
{
    public class DeleteMovieCommand : IRequest<int>
    {
        public DeleteMovieCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
