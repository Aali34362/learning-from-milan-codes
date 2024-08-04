using Domain.Abstractions;

namespace Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    private User()
    {
    }

    public Name Name { get; private set; }

    public static User Create(Name name)
    {
        var user = new User(Guid.NewGuid(), name);

        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}