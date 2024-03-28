using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WindowsSystem_ASP.NET.BL;
using WindowsSystem_ASP.NET.Commands;
using WindowsSystem_ASP.NET.DAL.Entities;
using WindowsSystem_ASP.NET.DAL.Models;
using WindowsSystem_ASP.NET.Queries;
using WindowsSystem_ASP.NET.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WindowsSystem_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _mediator.Send(new GetAllMoviesQuery());
            return Ok(movies);
        }

        // GET: Popular Movies
        [HttpGet("Popular")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetPopularMovies()
        {
            var movies = await _mediator.Send(new GetPopularMoviesQuery());
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {

            var movie = await _mediator.Send(new GetMovieByIdQuery { Id = id });
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);


        }

        // GET: by search
        [HttpGet("searchAll/query")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesBySearch(string s)
        {
           
            if (string.IsNullOrWhiteSpace(s))
            {
                return BadRequest("Search term is required.");
            }

            var movies = await _mediator.Send(new GetMoviesBySearchTermQuery(s));
            return Ok(movies);
        }
        // GET: by search
        [HttpGet("searchDB/query")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesBySearchFromDB(string s)
        {

            if (string.IsNullOrWhiteSpace(s))
            {
                return BadRequest("Search term is required.");
            }

            var movies = await _mediator.Send(new GetMovieInDatabaseBySearchQuery(s));
            return Ok(movies);
        }

        // POST: api/Movies
        [HttpPost("Add/Id")]
        public async Task<ActionResult<Movie>> PostMovie([FromQuery] int id)
        {
            var movieId = await _mediator.Send(new CreateMovieCommand(id));
            return Ok(movieId);

        }

        // PUT: api/Movies/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var success = await _mediator.Send(new UpdateMovieCommand(movie.Id,movie.Title, movie.Genres, movie.ReleaseDate, movie.RunTime, movie.Overview, movie.PosterURL, movie.TrailerURL, movie.TmdbId));

            if (success==0)
            {
                return NotFound();
            }

            return NoContent();
        }



        // DELETE: api/Movies/5
        [HttpDelete("Delete/Id")]
        public async Task<IActionResult> DeleteMovie([FromQuery] int id)
        {
            var success = await _mediator.Send(new DeleteMovieCommand(id));

            if (success==0)
            {
                return NotFound(); 
            }

            return Ok(); 
        }
    }
}
