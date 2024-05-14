using Callibrus.Server.Data;
using Callibrus.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Controllers;

[Route("api")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryDbContext _libraryDbContext;

    public BookController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    [HttpGet("books")]
    public async Task<IActionResult> GetBooks()
    {
        var books = _libraryDbContext.Books
            .Include(b => b.Authors);
        return Ok(await books.ToListAsync());
    }
    
    [HttpGet("book/{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _libraryDbContext.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (book == null)
            return NotFound();
        
        return Ok(book);
    }
    
    [HttpPut("book/{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book bookFromBody)
    {
        var book = await _libraryDbContext.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return NotFound($"Book with ID {id} not found.");
        }

        try
        {
            _libraryDbContext.Books.Update(book);
            await _libraryDbContext.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "Unable to save changes: " + ex.Message);
        }
    }
    
    [HttpDelete("book/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _libraryDbContext.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound($"Book with ID {id} not found.");
        }

        _libraryDbContext.Books.Remove(book);
        await _libraryDbContext.SaveChangesAsync();
        return NoContent();
    }
}