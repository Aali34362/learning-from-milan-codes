using CQRS.Tutorial.Repositories;

namespace CQRS.Tutorial.Books.Commands;

public class DeleteBookCommand
{
    public void Handle(Guid id)
    {
        InMemoryBookRepository.Books.Remove(id);
    }
}
