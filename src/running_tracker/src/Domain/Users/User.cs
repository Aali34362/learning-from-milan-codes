using SharedKernel;

namespace Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Email email, Name name, bool hasPublicProfile)
        : base(id)
    {
        Email = email;
        Name = name;
        HasPublicProfile = hasPublicProfile;
    }

    private User()
    {
    }

    public Email Email { get; private set; }

    public Name Name { get; private set; }

    public bool HasPublicProfile { get; set; }

    public static User Create(Email email, Name name, bool hasPublicProfile)
    {
        var user = new User(Guid.NewGuid(), email, name, hasPublicProfile);

        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
