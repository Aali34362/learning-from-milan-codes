using Application.Abstractions.Authentication;
using MediatR;

namespace Application.Customers.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _jwtProvider.GetForCredentialsAsync(request.Email, request.Password);
    }
}