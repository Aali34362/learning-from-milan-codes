using Gatherly.Domain.Repositories;
using Gatherly.Persistence.Interceptors;
using Gatherly.Persistence;
using Gatherly.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace Gatherly.App.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMemberRepository, MemberSqlRepository>();

        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        Gatherly.Infrastructure.AssemblyReference.Assembly,
                        Gatherly.Persistence.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(
                    configuration.GetConnectionString("Database"));
            });
    }
}
