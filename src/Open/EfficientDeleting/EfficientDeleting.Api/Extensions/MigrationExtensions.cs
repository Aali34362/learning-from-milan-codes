using EfficientDeleting.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace EfficientDeleting.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        dbContext.Database.Migrate();
    }
}
