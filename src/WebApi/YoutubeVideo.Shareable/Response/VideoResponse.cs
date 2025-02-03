using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.Shareable.Response
{
    public class VideoResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public VideoDto? Video { get; set; }
    }
}
