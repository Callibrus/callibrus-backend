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
            .Include(b => b.Authors)
            .Select(b => new
            {
                Title = b.Title,
                Description = b.Description,
                DatePublished = b.DatePublished,
                AvailableCopies = b.AvailableCopies,
                Genre = b.Genre,
                ImageUrl = b.ImageUrl,
                Authors = b.Authors.Select(Extensions.ToAuthorForBody)
            });
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
        
        return Ok(new
        {
            Title = book.Title,
            Description = book.Description,
            DatePublished = book.DatePublished,
            AvailableCopies = book.AvailableCopies,
            Genre = book.Genre,
            ImageUrl = book.ImageUrl,
            Authors = book.Authors.Select(Extensions.ToAuthorForBody)
        });
    }
    
    [HttpPost("book/create")]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest? newBookRequest)
    {
        if (newBookRequest == null)
        {
            return BadRequest("Book is null.");
        }

        try
        {
            await _libraryDbContext.Books.AddAsync(newBookRequest.ToBook());
            await _libraryDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = newBookRequest.Id }, newBookRequest);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "Unable to save changes: " + ex.Message);
        }
    }
    
    [HttpPut("book/update/{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] CreateBookRequest bookFromBody)
    {
        var book = await _libraryDbContext.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return NotFound($"Book with ID {id} not found.");
        }
            
        book = bookFromBody.ToBook();
        book.Id = id;
        
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
    
    [HttpDelete("book/delete/{id}")]
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