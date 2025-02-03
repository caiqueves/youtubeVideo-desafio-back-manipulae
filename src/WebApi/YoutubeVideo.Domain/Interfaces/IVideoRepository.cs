using YoutubeVideo.Domain.Entities;

namespace YoutubeVideo.Domain.Interfaces;

public interface IVideoRepository
{
    IQueryable<Video> GetVideos();
    Task<Video> GetVideo(int id);
    List<Video> GetVideoByTitle(string title);
    Task Add(Video video);
    Task Update(Video video);
}