using CQRS.Tutorial.Entities;
using CQRS.Tutorial.Repositories;

namespace CQRS.Tutorial.Books.Queries;

public class GetBookByIdQuery
{
    public Book? Handle(Guid id)
    {
        return InMemoryBookRepository.Books.TryGetValue(id, out var book) ? book : default;
    }
}
