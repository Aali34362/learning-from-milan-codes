using Application.Abstractions.Messaging;

namespace Application.Users.GetById;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
