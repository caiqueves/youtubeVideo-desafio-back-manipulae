using FluentValidation;
using YoutubeVideo.Shareable.Validators;

namespace YoutubeVideo.Shareable.DTOs;

public class VideoDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ChannelTitle { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Duration { get; set; }
    public DateTime PublishedAt { get; set; }

    public string LinkVideo { get; set; } = string.Empty;
}
