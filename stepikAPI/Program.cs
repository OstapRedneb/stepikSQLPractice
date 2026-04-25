var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // обязательно для Swagger
builder.Services.AddSwaggerGen();            // регистрация генератора

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();         // генерирует /swagger/v1/swagger.json
    app.UseSwaggerUI();       // показывает UI на /swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();