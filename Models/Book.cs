namespace Callibrus.Server.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DatePublished { get; set; }
    public string Genre { get; set; }
    public ICollection<Author>? Authors { get; set; }
}