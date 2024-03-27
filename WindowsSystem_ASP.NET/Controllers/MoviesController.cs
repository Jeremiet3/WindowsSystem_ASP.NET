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

            //var movie = await _dbContext.Movies.FindAsync(id);

            //if (movie == null)
            //{
            //    // Fetch from TMDb as a fallback
            //    var blMovie = await _tmdbApiService.GetMovieByIdAsync(id);
            //    if (blMovie == null)
            //    {
            //        return NotFound();
            //    }

            //    // Optional: Convert BL model back to entity model and save to database
            //    movie = BlConversion.GetMovie(blMovie);
            //    //_dbContext.Movies.Add(movie);
            //    //await _dbContext.SaveChangesAsync();

            //    return Ok(movie);
            //}

            //return Ok(movie);
            var movie = await _mediator.Send(new GetMovieByIdQuery { Id = id });
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);


        }

        // GET: by search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesBySearch(string s)
        {
            //var blMovie = await _tmdbApiService.SearchMoviesAsync(s);
            //if (blMovie == null)
            //{
            //    return NotFound();
            //}
            //List<Movie> movies = new List<Movie>();
            //foreach (var movie in blMovie)
            //{
            //    movies.Add(BlConversion.GetMovie(movie));
            //}
            //return Ok(movies);

            if (string.IsNullOrWhiteSpace(s))
            {
                return BadRequest("Search term is required.");
            }

            var movies = await _mediator.Send(new GetMoviesBySearchTermQuery(s));
            return Ok(movies);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            //_dbContext.Movies.Add(movie);
            //await _dbContext.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
            var movieId = await _mediator.Send(new CreateMovieCommand( movie.Title,movie.Genres,movie.ReleaseDate,movie.RunTime,movie.Overview,movie.PosterURL,movie.TrailerURL,movie.TmdbId));
            return Ok(movieId);

        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            //if (id != movie.Id)
            //{
            //    return BadRequest();
            //}
            //_dbContext.Entry(movie).State = EntityState.Modified;
            //try
            //{
            //    await _dbContext.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MovieExists(id))
            //        return NotFound();
            //    else
            //        throw;
            //}
            //return NoContent();
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var success = await _mediator.Send(new DeleteMovieCommand(id));

            if (success==0)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }
    }
}
