
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YoutubeVideo.Application.Interfaces;
using YoutubeVideo.Domain.Interfaces;
using YoutubeVideo.Shareable.Response;

namespace YoutubeVideo.Worker.Service
{
    public class YouTubeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<YouTubeService> _logger;
        private readonly IVideoRepository _videoRepository;

        public YouTubeService(HttpClient httpClient, ILogger<YouTubeService> logger, IVideoRepository videoRepository)
        {
            _httpClient = httpClient;
            _apiKey = "AIzaSyBjsMq1xgQcj9ZQfhw-R4oXRibZi1FUouc"; ////Environment.GetEnvironmentVariable("YOUTUBE_API_KEY");
            _logger = logger;
            _videoRepository = videoRepository;
        }

        public async Task<List<YoutubeItem>> BuscarVideosAsync()
        {
            var videos = new List<YoutubeItem>();

            try
            {
                string query = "medicamento";
                string url = $"https://www.googleapis.com/youtube/v3/search?q={query}&key={_apiKey}&part=snippet&maxResults=50";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                _logger.LogInformation(response.Content.ReadAsStringAsync().Result);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<YoutubeApiResponse>(jsonResponse);

                    foreach (var item in data.Items)
                    {

                        var video = new Domain.Entities.Video
                        {
                            Title = item.Snippet.Title,
                            Description = item.Snippet.Description,
                            ChannelTitle = item.Snippet.ChannelTitle,
                            PublishedAt = DateTime.Parse(item.Snippet.PublishedAt),
                            IsDeleted = false
                        };

                        // Verificar se o vídeo é de 2025
                        if (video.PublishedAt.Year == 2025)
                        {
                            await _videoRepository.AddAsync(video);
                        }
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
