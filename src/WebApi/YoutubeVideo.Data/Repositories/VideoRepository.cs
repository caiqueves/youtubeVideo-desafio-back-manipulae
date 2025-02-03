using System.Linq;
using Microsoft.EntityFrameworkCore;
using YoutubeVideo.Domain.Entities;
using YoutubeVideo.Domain.Interfaces;

namespace YoutubeVideo.Data.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        // Injeção de dependência do ApplicationDbContext
        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Video> GetVideos()
        {
            return _context.Videos
                           .Where(v => !v.IsDeleted).AsQueryable(); // Exemplo de filtro de vídeos não deletados
                           
        }

        public async Task<Video> GetVideo(int id)
        {
            return await _context.Videos
                           .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted); // Busca por ID e não deletado
        }

        public List<Video> GetVideoByTitle(string title)
        {
            return _context.Videos
                           .Where(v => v.Title.Contains(title) && !v.IsDeleted)
                           .ToList();
        }

        public async Task Add(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var video = _context.Videos.FirstOrDefault(v => v.Id == id);
            if (video != null)
            {
                video.IsDeleted = true;  // Delete lógico
                await _context.SaveChangesAsync();
            }
        }

    }
}