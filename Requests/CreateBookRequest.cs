namespace Callibrus.Server.Models;

public class CreateBookRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DatePublished { get; set; }
    public int AvailableCopies { get; set; }
    public string Genre { get; set; }
    public string? ImageUrl { get; set; }
}