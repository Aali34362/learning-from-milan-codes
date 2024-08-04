using MediatR;

namespace Application.Customers.Register;

public record RegisterCustomerCommand(string Email, string Password, string Name) : IRequest;

public record RegisterCustomerRequest(string Email, string Password, string Name);
