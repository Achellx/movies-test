using System.ComponentModel.DataAnnotations;
namespace backend.DTOs;

public class CreateDirectorDto
{
    [Required(ErrorMessage = "Name is mandatory")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nationality is mandatory")]
    public string Nationality { get; set; } = string.Empty;

    [Range(0, 130, ErrorMessage = "Age must be between 0 and 130")]
    public int Age { get; set; }
    public bool Active { get; set; } = true;
}