using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.DTOs;
using backend.Models;

namespace backend.Services;

public class MovieService : IMovieService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieDto>> GetAllAsync()
    {
        var movies = await _context.Movies
        .AsNoTracking()
        .Include(m => m.Director)
        .ToListAsync();
        return movies.Select(MapToDto);
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _context.Movies.AsNoTracking().Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == id);
        return movie is null ? null : MapToDto(movie);
    }

    public async Task<MovieDto?> CreateAsync(CreateMovieDto dto)
    {
        var directorExists = await _context.Directors.AnyAsync(d => d.Id == dto.FKDirector);
        if (!directorExists) return null;

        var movie = new Movie
        {
            Name = dto.Name,
            ReleaseYear = dto.ReleaseYear,
            Genre = dto.Genre,
            Duration = dto.Duration,
            FKDirector = dto.FKDirector
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        await _context.Entry(movie).Reference(m => m.Director).LoadAsync();
        return MapToDto(movie);
    }

    public async Task<MovieUpdateResult> UpdateAsync(int id, UpdateMovieDto dto)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie is null)
            return MovieUpdateResult.MovieNotFound;


        var directorExists = await _context.Directors.AnyAsync(d => d.Id == dto.FKDirector);
        if (!directorExists) return MovieUpdateResult.DirectorNotFound;

        movie.Name = dto.Name;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.Genre = dto.Genre;
        movie.Duration = dto.Duration;
        movie.FKDirector = dto.FKDirector;

        await _context.SaveChangesAsync();
        return MovieUpdateResult.Success;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null) return false;

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    private static MovieDto MapToDto(Movie m) => new ()
    {
        Id = m.Id,
        Name = m.Name,
        ReleaseYear = m.ReleaseYear,
        Genre = m.Genre,
        Duration = m.Duration,
        FKDirector = m.FKDirector,
        DirectorName = m.Director?.Name ?? String.Empty
    };
}