using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Training.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module endpoints service installer.
/// </summary>
internal sealed class EndpointsServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddControllers()
            .AddApplicationPart(Endpoints.AssemblyReference.Assembly);
}
