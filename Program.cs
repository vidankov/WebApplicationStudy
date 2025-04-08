var builder = WebApplication.CreateBuilder(args);

// добавляет сервис, который генерирует описание API на основе ваших контроллеров и эндпоинтов
builder.Services.AddEndpointsApiExplorer();
// добавляет генератор Swagger, который создает спецификацию OpenAPI.
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<DataContext>();
builder.Services.AddSingleton<ContactStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // добавляет middleware для генерации JSON-документа Swagger
    app.UseSwagger();
    // добавляет middleware для Swagger UI - веб-интерфейса, который позволяет просматривать и тестировать ваше API
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
