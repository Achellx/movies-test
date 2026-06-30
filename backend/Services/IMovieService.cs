using backend.DTOs;

namespace backend.Services;

public enum MovieUpdateResult
{
    Success,
    MovieNotFound,
    DirectorNotFound
}

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllAsync();
    Task<MovieDto?> GetByIdAsync(int id);
    Task<MovieDto?> CreateAsync(CreateMovieDto dto);
    Task<MovieUpdateResult> UpdateAsync(int id, UpdateMovieDto dto);
    Task<bool> DeleteAsync(int id);
}