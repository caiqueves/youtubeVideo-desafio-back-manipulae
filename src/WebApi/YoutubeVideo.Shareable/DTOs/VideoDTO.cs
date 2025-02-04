using FluentValidation;
using YoutubeVideo.Shareable.Validators;

namespace YoutubeVideo.Shareable.DTOs;

public class VideoDto
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ChannelTitle { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Duration { get; set; }
    public DateTime PublishedAt { get; set; }
    public string Link { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
