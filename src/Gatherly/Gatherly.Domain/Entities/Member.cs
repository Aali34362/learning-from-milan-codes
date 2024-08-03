using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : AggregateRoot, IAuditableEntity
{
    private Member(Guid id, Email email, FirstName firstName, LastName lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    private Member()
    {
    }

    public Email Email { get; private set; }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public ICollection<Role> Roles { get; set; } = new List<Role>();

    public static Member Create(
        Guid id,
        Email email,
        FirstName firstName,
        LastName lastName)
    {
        var member = new Member(
            id,
            email,
            firstName,
            lastName);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(
            Guid.NewGuid(),
            member.Id));

        return member;
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
        {
            RaiseDomainEvent(new MemberNameChangedDomainEvent(
                Guid.NewGuid(), Id));
        }

        FirstName = firstName;
        LastName = lastName;
    }

    public MemberSnapshot ToSnapshot()
    {
        return new MemberSnapshot
        {
            Id = Id,
            Email = Email.Value,
            FirstName = FirstName.Value,
            LastName = LastName.Value,
            CreatedOnUtc = CreatedOnUtc,
            ModifiedOnUtc = ModifiedOnUtc
        };
    }

    public static Member FromSnapshot(MemberSnapshot memberSnapshot)
    {
        return new Member
        {
            Id = memberSnapshot.Id,
            Email = Email.Create(memberSnapshot.Email).Value,
            FirstName = FirstName.Create(memberSnapshot.FirstName).Value,
            LastName = LastName.Create(memberSnapshot.LastName).Value,
            CreatedOnUtc = memberSnapshot.CreatedOnUtc,
            ModifiedOnUtc = memberSnapshot.ModifiedOnUtc
        };
    }
}
