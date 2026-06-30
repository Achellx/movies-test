namespace backend.Models;

public class Director
{
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty; 
    public int Age { get; set; }
    public bool Active { get; set; }
}