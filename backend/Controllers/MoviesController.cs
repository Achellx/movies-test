using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    // GET: api/v1/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
    {
        var movies = await _service.GetAllAsync();
        return Ok(movies);
    }

    // GET: api/v1/movies/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDto>> GetById(int id)
    {
        var movie = await _service.GetByIdAsync(id);
        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    // POST: api/v1/movies
    [HttpPost]
    public async Task<ActionResult<MovieDto>> Create(CreateMovieDto dto)
    {
        var created = await _service.CreateAsync(dto);
        if (created is null) return BadRequest($"The director with id {dto.FKDirector} does not exist.");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/v1/movies/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<MovieDto>> Update(int id, UpdateMovieDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result switch
        {
            MovieUpdateResult.Success => NoContent(),
            MovieUpdateResult.MovieNotFound => NotFound($"The movie with id {id} does not exist."),
            MovieUpdateResult.DirectorNotFound => NotFound($"The director with {dto.FKDirector} does not exist."),
            _ => StatusCode(500)
        };
    }

    // DELETE: api/v1/movies/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}