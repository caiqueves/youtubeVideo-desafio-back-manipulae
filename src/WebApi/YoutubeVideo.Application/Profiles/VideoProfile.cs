using AutoMapper;
using YoutubeVideo.Domain.Entities;
using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.Application.Profiles;

public class VideoProfile : Profile
{
    public VideoProfile()
    {
        CreateMap<Video, VideoDto>();
        CreateMap<VideoDto, Video>();
    }
}

