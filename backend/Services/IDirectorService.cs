using backend.DTOs;

namespace backend.Services;

public interface IDirectorService
{
    Task<IEnumerable<DirectorDto>> GetAllAsync();
    Task<DirectorDto?> GetByIdAsync(int id);
    Task<DirectorDto> CreateAsync(CreateDirectorDto dto);
    Task<bool> UpdateAsync(int id, UpdateDirectorDto dto);
    Task<bool> DeleteAsync(int id);
}