using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommand : IRequest, ICommandBase
{
}

public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
{
}