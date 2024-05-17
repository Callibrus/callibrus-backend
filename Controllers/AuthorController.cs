using Callibrus.Server.Data;
using Callibrus.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Controllers;

[Route("api")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly LibraryDbContext _libraryDbContext;

    public AuthorController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    [HttpGet("authors")]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = _libraryDbContext.Authors
            .Include(b => b.Books);
        return Ok(await authors.ToListAsync());
    }
    
    [HttpGet("author/{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var author = await _libraryDbContext.Authors
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (author == null)
            return NotFound();
        
        return Ok(author);
    }
    
    [HttpPost("author/create")]
    public async Task<IActionResult> AddAuthor([FromBody] Author? newAuthor)
    {
        if (newAuthor == null)
        {
            return BadRequest("Author is null.");
        }

        try
        {
            await _libraryDbContext.Authors.AddAsync(newAuthor);
            await _libraryDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "Unable to save changes: " + ex.Message);
        }
    }
    
    [HttpPut("author/update/{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author authorFromBody)
    {
        var author = await _libraryDbContext.Authors
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (author == null)
        {
            return NotFound($"Author with ID {id} not found.");
        }

        try
        {
            _libraryDbContext.Authors.Update(author);
            await _libraryDbContext.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "Unable to save changes: " + ex.Message);
        }
    }
    
    [HttpDelete("author/delete/{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _libraryDbContext.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound($"Author with ID {id} not found.");
        }

        _libraryDbContext.Authors.Remove(author);
        await _libraryDbContext.SaveChangesAsync();
        return NoContent();
    }
}