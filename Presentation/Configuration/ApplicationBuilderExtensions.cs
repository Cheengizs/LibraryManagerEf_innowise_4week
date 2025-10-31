using Infrastructure.Seed;

namespace LibraryManagerEf.Configuration;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
        await seeder.SeedAsync();
    }
}