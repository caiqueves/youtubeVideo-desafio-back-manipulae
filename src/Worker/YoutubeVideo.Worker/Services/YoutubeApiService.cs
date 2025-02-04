using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YoutubeVideo.Worker.Models;
using YoutubeVideo.Worker.Repositories;

namespace YoutubeVideo.Worker.Services
{
    public class YoutubeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<YoutubeApiService> _logger;

        public YoutubeApiService(HttpClient httpClient, ILogger<YoutubeApiService> logger)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("api_key");
            _logger = logger;
        }

        public async Task<List<Video>> BuscarVideosAsync()
        {
            var videos = new List<Video>();

            try
            {
                _logger.LogInformation("[BuscarVideos] Requisitando dados de videos para api do youtube");

                string query = "manipulação+de+medicamentos";
                string url = $"https://www.googleapis.com/youtube/v3/search?q={query}&key={_apiKey}&part=snippet&maxResults=100&regionCode=BR&relevanceLanguage=pt&publishedAfter=2025-01-01T00:00:00Z&type=video";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("[BuscarVideos] Dados retornados com sucesso da api do youtube");

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Root>(jsonResponse);

                    foreach (var item in data!.items)
                    {
                       
                        var video = new Video
                        {
                            Title = item.snippet.title,
                            Description = item.snippet.description,
                            ChannelTitle = item.snippet.channelTitle,
                            PublishedAt = item.snippet.publishedAt,
                            LinkVideo = item.snippet.thumbnails.@default.url,
                            IsDeleted = false,

                        };
                        Console.WriteLine(JsonConvert.SerializeObject(video));
                        if (video.PublishedAt.Year == 2025)
                        {
                            if (VideosRepository.GetVideoTitle(video.Title).Select().Length == 0)
                            {
                                videos.Add(video);
                               
                            }
                        }

                        _logger.LogInformation("[BuscarVideos] Dados salvo com sucesso no banco de dados");
                    }

                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar vídeos na base do youtube: {ex.Message}");
            }

            return videos;
        }
    }
}
