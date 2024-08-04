using CQRS.Tutorial.Entities;
using CQRS.Tutorial.Repositories;

namespace CQRS.Tutorial.Books.Queries;

public class GetBooksQuery
{
    public List<Book> Handle()
    {
        return InMemoryBookRepository.Books.Values.ToList();
    }
}
