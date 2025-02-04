using YoutubeVideo.Shareable.DTOs;
using YoutubeVideo.Shareable.Response;

namespace YoutubeVideo.Application.Interfaces;

public interface IVideoService
{
        Task<FilterVideoResponse> FilterVideosAsync(VideoFilterDto filterDto);
        Task<VideoResponse> GetVideoByIdAsync(BuscarIdDTO id);
        Task<VideoResponse> InsertVideoAsync(VideoDto videoDto);
        Task<VideoResponse> UpdateVideoAsync(string id, VideoDto videoDto);
        Task<VideoResponse> DeleteVideoAsync(BuscarIdDTO id);
}
