public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollection(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        // добавляет сервис, который генерирует описание API на основе ваших контроллеров и эндпоинтов
        services.AddEndpointsApiExplorer();
        // добавляет генератор Swagger, который создает спецификацию OpenAPI.
        services.AddSwaggerGen();

        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        var connectionString = configuration.GetConnectionString("SqliteStringConnection");
        services.AddSingleton<IStorage>(new SqliteStorage(connectionString)); // p => new InMemoryStorage(10)

        return services;
    }
}
