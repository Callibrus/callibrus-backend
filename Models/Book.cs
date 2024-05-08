namespace Callibrus.Server.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public ICollection<Author> Authors { get; set; }
}