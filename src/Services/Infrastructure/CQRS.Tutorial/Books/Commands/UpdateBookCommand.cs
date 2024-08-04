using CQRS.Tutorial.Entities;
using CQRS.Tutorial.Repositories;

namespace CQRS.Tutorial.Books.Commands;

public class UpdateBookCommand
{
    public void Handle(Book book)
    {
        if (!InMemoryBookRepository.Books.TryGetValue(book.Id, out var existingBook))
        {
            return;
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.NumberOfPages = book.NumberOfPages;
    }
}
