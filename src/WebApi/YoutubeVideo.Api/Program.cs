using FluentValidation.AspNetCore;
using YoutubeVideo.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

var dbPath = Path.Combine("./src/", "videos.db");

builder.Services.AddDependencies(dbPath);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "YouTube Video API v1");
        c.RoutePrefix = string.Empty; 
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
