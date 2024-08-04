using CQRS.Tutorial.Entities;

namespace CQRS.Tutorial.Repositories;

public static class InMemoryBookRepository
{
    public static readonly Dictionary<Guid, Book> Books = new();
}
