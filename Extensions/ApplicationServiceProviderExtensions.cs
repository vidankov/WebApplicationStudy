public static class ApplicationServiceProviderExtensions
{
    public static IServiceProvider AddCustomService(
        this IServiceProvider services, IConfiguration configuration)
    {
        using var scope = services.CreateScope();

        var dbStorage = scope.ServiceProvider.GetService<IStorage>() as SqliteStorage;

        if (dbStorage != null)
        {
            string? connectionString = configuration.GetConnectionString("SqliteStringConnection");

            new FakerInitializer(connectionString).Initialize();
        }

        return services;
    }
}
