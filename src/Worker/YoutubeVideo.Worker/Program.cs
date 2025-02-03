using YoutubeVideo.Worker;
using YoutubeVideo.Worker.Repositories;
using YoutubeVideo.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<VideosRepository>();
builder.Services.AddHttpClient<YoutubeApiService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
