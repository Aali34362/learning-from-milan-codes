using MediatR;

namespace Application.Customers.Create;

public record CreateCustomerCommand(string Email, string Name) : IRequest;

public record CreateCustomerRequest(string Email, string Name);
