using App.Extensions;
using App.Utility;
using Infrastructure.Extensions;
using Serilog;

LoggingUtility.Run(() =>
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services
        .InstallServicesFromAssemblies(
            builder.Configuration,
            App.AssemblyReference.Assembly,
            Authorization.AssemblyReference.Assembly,
            Persistence.AssemblyReference.Assembly)
        .InstallModulesFromAssemblies(
            builder.Configuration,
            Modules.Users.Infrastructure.AssemblyReference.Assembly,
            Modules.Training.Infrastructure.AssemblyReference.Assembly,
            Modules.Notifications.Infrastructure.AssemblyReference.Assembly);

    builder.Host.UseSerilogWithConfiguration();

    WebApplication webApplication = builder.Build();

    webApplication
        .UseSwagger()
        .UseSwaggerUI()
        .UseCors(corsPolicyBuilder =>
            corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

    webApplication.UseSerilogRequestLogging()
        .UseHttpsRedirection()
        .UseAuthentication()
        .UseAuthorization();

    webApplication.MapControllers();

    webApplication.Run();
});
