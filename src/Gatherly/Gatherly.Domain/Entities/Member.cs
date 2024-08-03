using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : Entity
{
    public Member(Guid id, string email, FirstName firstName, string lastName)
         : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public FirstName FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}