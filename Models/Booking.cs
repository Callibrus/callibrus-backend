namespace Callibrus.Server.Models;

public class Booking
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; }
}