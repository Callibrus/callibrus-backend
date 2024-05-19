using Callibrus.Server.Data;
using Callibrus.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Controllers;

[Route("api")]
[ApiController]
[EnableCors("_myAllowSpecificOrigins")]
public class BookingController : ControllerBase
{
    private readonly LibraryDbContext _libraryDbContext;

    public BookingController(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    [HttpGet("bookings")]
    public async Task<IActionResult> GetBookings()
    {
        var bookings =  _libraryDbContext.Bookings;
        return Ok(await bookings.ToListAsync());
    }
    
    [HttpGet("bookings/bookId={bookId}")]
    public async Task<IActionResult> GetBookingsByBookId(int bookId)
    {
        var bookings = _libraryDbContext.Bookings
            .Where(b => b.BookId == bookId);
        return Ok(await bookings.ToListAsync());
    }
    
    [HttpGet("booking/{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var booking = await _libraryDbContext.Bookings
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (booking == null)
            return NotFound();
        
        return Ok(booking);
    }
    
    [HttpPost("booking/create")]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest? newBookingRequest)
    {
        if (newBookingRequest == null)
        {
            return BadRequest("Booking is null.");
        }

        try
        {
            await _libraryDbContext.Bookings.AddAsync(newBookingRequest.ToBooking());
            await _libraryDbContext.SaveChangesAsync();

            var book = _libraryDbContext.Bookings
                .FirstOrDefaultAsync(b => b.BookId == newBookingRequest.BookId && b.UserName == newBookingRequest.UserName);
            return CreatedAtAction(nameof(GetBookingById), new { id = book.Id }, book);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "Unable to save changes: " + ex.Message);
        }
    }
    
    [HttpDelete("booking/delete/{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var booking = await _libraryDbContext.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound("Booking not found");
        }
    
        _libraryDbContext.Bookings.Remove(booking);
        await _libraryDbContext.SaveChangesAsync();
    
        return NoContent();
    }
    
    [HttpOptions("booking/create")]
    public IActionResult Options()
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        Response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
        Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        return Ok();
    }
}