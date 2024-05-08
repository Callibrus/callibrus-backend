using Callibrus.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
}