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
    
    public static CreateBookRequest ToBookForBody(this Book book)
    {
        return new CreateBookRequest()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            DatePublished = book.DatePublished,
            AvailableCopies = book.AvailableCopies,
            Genre = book.Genre,
            ImageUrl = book.ImageUrl
        };
    }

    public static CreateAuthorRequest ToAuthorForBody(this Author author)
    {
        return new CreateAuthorRequest()
        {
            Id = author.Id,
            FullName = author.FullName,
            BirthDate = author.BirthDate,
            DeathDate = author.DeathDate,
            Biography = author.Biography,
            ImageUrl = author.ImageUrl
        };
    }

    public static CreateBookingRequest ToBookingForBody(this Booking booking)
    {
        return new CreateBookingRequest()
        {
            Id = booking.Id,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            UserName = booking.UserName,
            BookId = booking.BookId
        };
    }
}