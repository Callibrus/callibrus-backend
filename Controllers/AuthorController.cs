using Callibrus.Server.Data;
using Callibrus.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Controllers;

[Route("api")]
[ApiController]
[EnableCors("_myAllowSpecificOrigins")]
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
            .Include(b => b.Books)
            .Select(a => new
            {
                Id = a.Id,
                FullName = a.FullName,
                BirthDate = a.BirthDate,
                DeathDate = a.DeathDate,
                Biography = a.Biography,
                ImageUrl = a.ImageUrl,
                Books = a.Books.Select(a => a.ToBookForBody())
            });
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
        
        return Ok(new
        {
            Id = author.Id,
            FullName = author.FullName,
            BirthDate = author.BirthDate,
            DeathDate = author.DeathDate,
            Biography = author.Biography,
            ImageUrl = author.ImageUrl,
            Books = author.Books.Select(a => a.ToBookForBody())
        });
    }
    
    [EnableCors("_myAllowSpecificOrigins")]
    [HttpPost("author/create")]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest? newAuthorRequest)
    {
        if (newAuthorRequest == null)
        {
            return BadRequest("Author is null.");
        }

        try
        {
            await _libraryDbContext.Authors.AddAsync(newAuthorRequest.ToAuthor());
            await _libraryDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthorRequest.Id }, newAuthorRequest);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "Unable to save changes: " + ex.Message);
        }
    }
    
    [EnableCors("_myAllowSpecificOrigins")]
    [HttpPut("author/update/{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] CreateAuthorRequest authorFromBody)
    {
        var author = await _libraryDbContext.Authors
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (author == null)
        {
            return NotFound($"Author with ID {id} not found.");
        }

        author = authorFromBody.ToAuthor();
        author.Id = id;
        
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
    
    [EnableCors("_myAllowSpecificOrigins")]
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