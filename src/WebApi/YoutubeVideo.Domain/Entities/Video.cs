
namespace YoutubeVideo.Domain.Entities;
public class Video
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ChannelTitle { get; set; }
    public string Author { get; set; }
    public string Duration { get; set; }
    public DateTime PublishedAt { get; set; }
    public string Link { get; set; }
    public bool IsDeleted { get; set; }
}