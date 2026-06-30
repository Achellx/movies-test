using System.ComponentModel.DataAnnotations;
namespace backend.DTOs;

public class CreateMovieDto
{
    [Required(ErrorMessage = "Name is mandatory")]
    public string Name { get; set; } = string.Empty;

    [Range(1888, 2100, ErrorMessage = "Release year must be between 1888 and 2100")]
    public int ReleaseYear { get; set; }

    [Required(ErrorMessage = "Genre is mandatory")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Duration is mandatory")]
    public TimeSpan Duration { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Director must be a valid director ID")]
    public int FKDirector { get; set; }
}