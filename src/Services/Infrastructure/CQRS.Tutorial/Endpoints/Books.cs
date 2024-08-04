using CQRS.Tutorial.Books.Commands;
using CQRS.Tutorial.Books.Queries;
using CQRS.Tutorial.Entities;

namespace CQRS.Tutorial.Endpoints;

public static class Books
{
    public static void AddBooksEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("books", () =>
        {
            return Results.Ok(new GetBooksQuery().Handle());
        });

        app.MapGet("books/{id}", (Guid id) =>
        {
            return Results.Ok(new GetBookByIdQuery().Handle(id));
        });

        app.MapPost("books", (Book book) =>
        {
            new AddBookCommand().Handle(book);

            return Results.Ok();
        });

        app.MapPut("books", (Book book) =>
        {
            new UpdateBookCommand().Handle(book);

            return Results.NoContent();
        });

        app.MapDelete("books/{id}", (Guid id) =>
        {
            new DeleteBookCommand().Handle(id);

            return Results.NoContent();
        });
    }
}
