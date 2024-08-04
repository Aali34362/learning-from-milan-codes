using CQRS.Tutorial.Entities;
using CQRS.Tutorial.Repositories;

namespace CQRS.Tutorial.Books.Commands;

public class AddBookCommand
{
    public void Handle(Book book)
    {
        InMemoryBookRepository.Books.Add(book.Id, book);
    }
}
