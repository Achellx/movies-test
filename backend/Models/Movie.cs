namespace backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public bool Active { get; set; } = true;
    public int FKDirector { get; set; }    
    [ForeignKey(nameof(FKDirector))]
    public Director Director { get; set; } = null!;

}