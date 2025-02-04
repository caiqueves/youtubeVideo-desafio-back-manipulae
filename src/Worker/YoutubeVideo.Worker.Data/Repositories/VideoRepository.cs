using YoutubeVideo.Domain.Entities;
using YoutubeVideo.Domain.Interfaces;

namespace YoutubeVideo.Data.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly ApplicationDbContext _context;

    public VideoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Video> GetVideos()
    {
        return _context.Videos!.AsQueryable();
    }

    public async Task AddAsync(Video video)
    {
        _context.Videos!.Add(video);
        await _context.SaveChangesAsync();
    }

    public async Task<Video> GetByIdAsync(int id)
    {
#pragma warning disable CS8603 // Possível retorno de referência nula.
        return await _context.Videos!.FindAsync(id);
#pragma warning restore CS8603 // Possível retorno de referência nula.
    }

    public async Task UpdateAsync(Video video)
    {
        _context.Videos!.Update(video);
        await _context.SaveChangesAsync();
    }
}