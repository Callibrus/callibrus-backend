namespace Callibrus.Server.Models;

public static class Extensions
{
    public static Book ToBook(this CreateBookRequest createBookRequest)
    {
        return new Book
        {
            Id = createBookRequest.Id,
            Title = createBookRequest.Title,
            Description = createBookRequest.Description,
            DatePublished = createBookRequest.DatePublished,
            AvailableCopies = createBookRequest.AvailableCopies,
            Genre = createBookRequest.Genre,
            ImageUrl = createBookRequest.ImageUrl
        };
    }

    public static Author ToAuthor(this CreateAuthorRequest createAuthorRequest)
    {
        return new Author()
        {
            Id = createAuthorRequest.Id,
            FullName = createAuthorRequest.FullName,
            BirthDate = createAuthorRequest.BirthDate,
            DeathDate = createAuthorRequest.DeathDate,
            Biography = createAuthorRequest.Biography,
            ImageUrl = createAuthorRequest.ImageUrl
        };
    }

    public static Booking ToBooking(this CreateBookingRequest createBookingRequest)
    {
        return new Booking()
        {
            Id = createBookingRequest.Id,
            StartTime = createBookingRequest.StartTime,
            EndTime = createBookingRequest.EndTime,
            UserName = createBookingRequest.UserName,
            BookId = createBookingRequest.BookId
        };
    }
}