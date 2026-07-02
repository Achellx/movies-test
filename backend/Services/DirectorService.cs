using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.DTOs;
using backend.Models;

namespace backend.Services;

public class DirectorService : IDirectorService
{
    private readonly AppDbContext _context;

    public DirectorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DirectorDto>> GetAllAsync()
    {
        var directors = await _context.Directors
            .AsNoTracking()
            .Where(d => d.Active)
            .OrderBy(d => d.Name)
            .ToListAsync();
        return directors.Select(MapToDto);
    }

    public async Task<DirectorDto?> GetByIdAsync(int id)
    {
        var director = await _context.Directors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id && d.Active);
        return director is null ? null : MapToDto(director);
    }

    public async Task<DirectorDto> CreateAsync(CreateDirectorDto dto)
    {
        var director = new Director
        {
            Name = dto.Name,
            Nationality = dto.Nationality,
            Age = dto.Age,
            Active = dto.Active
        };

        _context.Directors.Add(director);
        await _context.SaveChangesAsync();

        return MapToDto(director);
    }

    public async Task<bool> UpdateAsync(int id, UpdateDirectorDto dto)
    {
        var director = await _context.Directors.FindAsync(id);
        
        if (director is null)
            return false;

        director.Name = dto.Name;
        director.Nationality = dto.Nationality;
        director.Age = dto.Age;
        director.Active = dto.Active;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var director = await _context.Directors.FindAsync(id);
        if (director is null) return false;

        director.Active = false;
        await _context.SaveChangesAsync();
        return true;
    }

    private static DirectorDto MapToDto(Director d) => new ()
    {
        Id = d.Id,
        Name = d.Name,
        Nationality = d.Nationality,
        Age = d.Age,
        Active = d.Active
    };
}