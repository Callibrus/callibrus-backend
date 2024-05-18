namespace Callibrus.Server.Models;

public class CreateBookingRequest
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string UserName { get; set; }
}