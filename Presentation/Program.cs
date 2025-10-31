using LibraryManagerEf.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();