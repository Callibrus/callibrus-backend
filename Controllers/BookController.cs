using Callibrus.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryDbContext _libraryDbContext;

    public BookController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = _libraryDbContext.Books;
        return Ok(await books.ToListAsync());
    }
}