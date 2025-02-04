using FluentValidation;
using YoutubeVideo.Shareable.Validators;

namespace YoutubeVideo.Shareable.DTOs;

public class VideoFilterDto
{
    public string? Title { get; set; }
    public string? Duration { get; set; } 
    public string? Author { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public string? Q { get; set; }
}