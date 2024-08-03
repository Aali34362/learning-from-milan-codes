namespace Gatherly.App.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddApplicationPart(Gatherly.Presentation.AssemblyReference.Assembly);

        services.AddSwaggerGen();
    }
}
