using System.Data;
using YoutubeVideo.Domain.Entities;

namespace YoutubeVideo.Domain.Interfaces;

public interface IVideoRepository
{
    Task<IQueryable<Video>> GetAllAsync();
    Task<Video> GetByIdAsync(string id);
    Task AddAsync(Video video);
    Task UpdateAsync(Video video);
    Task DeleteAsync(string id);
}