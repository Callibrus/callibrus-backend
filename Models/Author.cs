using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Callibrus.Server.Models;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string Biography { get; set; }
    public string? ImageUrl { get; set; }
    
    public ICollection<Book>? Books { get; set; }
}