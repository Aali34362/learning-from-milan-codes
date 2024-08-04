using System.Collections.Concurrent;
using Application.EventBus;
using Infrastructure.Utilities;
using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.BackgroundJobs.ProcessInboxMessages;

/// <summary>
/// Represents the integration event handler factory.
/// </summary>
internal static class IntegrationEventHandlerFactory
{
    private static readonly ConcurrentDictionary<Type, List<Type>> EventHandlersDictionary = new();

    /// <summary>
    /// Gets the handlers for the specified type.
    /// </summary>
    /// <param name="type">The integration event type.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>THe collection of <see cref="IIntegrationEventHandler"/> instance that handle the specified integration event type.</returns>
    internal static IEnumerable<IIntegrationEventHandler> GetHandlers(Type type, IServiceProvider serviceProvider)
    {
        if (!EventHandlersDictionary.ContainsKey(type))
        {
            AddHandlersToDictionary(type);
        }

        foreach (Type eventHandlerType in EventHandlersDictionary[type])
        {
            yield return (serviceProvider.GetService(eventHandlerType) as IIntegrationEventHandler)!;
        }
    }

    private static void AddHandlersToDictionary(Type type) =>
        Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(EventHandlersUtility.ImplementsIntegrationEventHandler)
            .ForEach(integrationEventHandlerType =>
            {
                Type closedIntegrationEventHandler = integrationEventHandlerType
                    .GetInterfaces()
                    .First(EventHandlersUtility.IsIntegrationEventHandler);

                Type[] arguments = closedIntegrationEventHandler.GetGenericArguments();

                if (arguments[0] != type)
                {
                    return;
                }

                EventHandlersDictionary.AddOrUpdate(
                    type,
                    _ => new List<Type> { integrationEventHandlerType },
                    (_, handlersList) =>
                    {
                        handlersList.Add(integrationEventHandlerType);

                        return handlersList;
                    });
            });
}
