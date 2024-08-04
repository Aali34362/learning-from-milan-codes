using Application.Abstractions.Authentication;
using FirebaseAdmin.Auth;

namespace Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public async Task<string> RegisterAsync(string email, string password)
    {
        var userArgs = new UserRecordArgs
        {
            Email = email,
            Password = password
        };

        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        return userRecord.Uid;
    }
}