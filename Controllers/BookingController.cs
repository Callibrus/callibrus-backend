using Callibrus.Server.Data;
using Callibrus.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Callibrus.Server.Controllers;

[Route("api")]
[ApiController]
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
        var bookings = _libraryDbContext.Bookings;
        return Ok(bookings);
    }
    
    [HttpGet("booking/{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var booking = _libraryDbContext.Bookings
            .FirstOrDefaultAsync(b => b.Id == id);
        return Ok(booking);
    }
    
    [HttpPost("booking/create")]
    public async Task<IActionResult> AddBooking([FromBody] Booking? newBooking)
    {
        if (newBooking == null)
        {
            return BadRequest("Booking is null.");
        }

        try
        {
            await _libraryDbContext.Bookings.AddAsync(newBooking);
            await _libraryDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookingById), new { id = newBooking.Id }, newBooking);
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
}