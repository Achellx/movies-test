namespace backend.DTOs;

public class MovieDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public bool Active { get; set; }
    public int FKDirector { get; set; }
    public string DirectorName { get; set; } = string.Empty;
}