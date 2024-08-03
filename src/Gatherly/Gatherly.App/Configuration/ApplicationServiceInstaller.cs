using FluentValidation;
using Gatherly.Application.Behaviors;
using Gatherly.Infrastructure.Idempotence;
using MediatR;

namespace Gatherly.App.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Gatherly.Application.AssemblyReference.Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddValidatorsFromAssembly(
            Gatherly.Application.AssemblyReference.Assembly,
            includeInternalTypes: true);
    }
}
