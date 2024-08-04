using Gatherly.App.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddInfrastructure();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddBackgroundJobs();

builder.Services.AddPresentation();

builder.Services.AddAuthenticationAndAuthorization();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
