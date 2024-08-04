using Application.EventBus;
using Application.Time;
using Infrastructure.Configuration;
using Infrastructure.EventBus;
using Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Modules.Notifications.Application.Abstractions.Email;
using Modules.Notifications.Infrastructure.Email;
using Modules.Notifications.Infrastructure.Email.Abstractions;
using Modules.Notifications.Infrastructure.Email.Configuration;
using Modules.Notifications.Infrastructure.Email.DelegatingHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the user's module infrastructure service installer.
/// </summary>
internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .Tap(services.TryAddTransient<ISystemTime, SystemTime>)
            .Tap(services.TryAddTransient<IEventBus, EventBus>)
            .Tap(AddMailerSend);

    private static void AddMailerSend(IServiceCollection services) =>
        services
            .ConfigureOptions<MailersendOptionsSetup>()
            .AddTransient<MailersendAuthorizationDelegatingHandler>()
            .AddTransient<IEmailSender, EmailSender>()
            .AddRefitClient<IMailersendClient>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    })
            })
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                MailersendOptions mailersendOptions = serviceProvider.GetService<IOptions<MailersendOptions>>()!.Value;

                httpClient.BaseAddress = new Uri(mailersendOptions.BaseUrl);
            })
            .AddHttpMessageHandler<MailersendAuthorizationDelegatingHandler>();
}
