using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Threading.Channels;
using YoutubeVideo.Worker.Models;
using YoutubeVideo.Worker.Repositories;
using YoutubeVideo.Worker.Services;

namespace YoutubeVideo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly YoutubeApiService _youtubeApiService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, YoutubeApiService youtubeApiService, IServiceScopeFactory serviceScopeFactory )
        {
            _logger = logger;
            _youtubeApiService = youtubeApiService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Buscando vídeos do YouTube às: {time}", DateTimeOffset.Now);

                if (!File.Exists(@"c:\temp\\Videos.sqlite"))
                {
                    // O arquivo não existe, então cria o banco de dados e a tabela
                    VideosRepository.CriarBancoSQLite();
                    VideosRepository.CriarTabelaSQlite();
                }

                var listVideos = await _youtubeApiService.BuscarVideosAsync();

                using (var scope = _serviceScopeFactory.CreateScope()) // Criando um escopo
                {
                   
                    foreach (var item in listVideos)
                    {
                        if (item != null)
                        {
                            VideosRepository.Add(item);
                        }
                    }
                }

                // Wait 10 minutes before fetching new videos
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}

