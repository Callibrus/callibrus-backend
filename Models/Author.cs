using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Callibrus.Server.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Nickname { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string Biography { get; set; }
    
    public ICollection<Book>? Books { get; set; }
}