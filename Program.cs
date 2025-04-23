var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceCollection(builder.Configuration);

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
