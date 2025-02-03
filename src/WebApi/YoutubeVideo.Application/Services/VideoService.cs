
using AutoMapper;
using YoutubeVideo.Application.Interfaces;
using YoutubeVideo.Domain.Entities;
using YoutubeVideo.Domain.Interfaces;
using YoutubeVideo.Shareable.DTOs;
using YoutubeVideo.Shareable.Response;

namespace YoutubeVideo.Application.Services;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;
    private readonly IMapper _mapper;

    public VideoService(IVideoRepository videoRepository, IMapper autoMapper)
    {
        _videoRepository = videoRepository;
        _mapper = autoMapper;
    }

    public async Task<FilterVideoResponse> FilterVideosAsync(VideoFilterDto filterDto)
    {
        try
        {
            var filterVideoResponse = new FilterVideoResponse();

            var query = _videoRepository.GetVideos();

            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                query = query.Where(v => v.Title.Contains(filterDto.Title));
            }

            if(!string.IsNullOrEmpty(filterDto.Duration))
            {
                query = query.Where(v => v.Duration == filterDto.Duration);
            }

            if (!string.IsNullOrEmpty(filterDto.Author))
            {
                query = query.Where(v => v.Author.Contains(filterDto.Author));
            }

            if (filterDto.CreatedAfter.HasValue)
            {
                query = query.Where(v => v.PublishedAt >= filterDto.CreatedAfter.Value);
            }

            if (!string.IsNullOrEmpty(filterDto.Q))
            {
                query = query.Where(v => v.Title.Contains(filterDto.Q) ||
                                          v.Description.Contains(filterDto.Q) ||
                                          v.ChannelTitle.Contains(filterDto.Q));
            }

            if (query != null)
            {
                List<VideoDto> videoFilters = new List<VideoDto>();

                foreach (var item in query.ToList())
                {
                    videoFilters.Add(_mapper.Map<VideoDto>(item));
                }

                filterVideoResponse.StatusCode = 200;
                filterVideoResponse.Videos = videoFilters;
                filterVideoResponse.Message = "Filtro realizado com sucesso.";

                return filterVideoResponse;
            }
            else
            {
                filterVideoResponse.StatusCode = 404;
                filterVideoResponse.Message = "Não foi encontrado nenhum video";

                return filterVideoResponse;
            }
        }
        catch (Exception ex)
        {
            return new FilterVideoResponse
            {
                StatusCode = 500,
                Message = $"Erro ao tentar buscar os videos {ex.Message}"
            };
        }
    }

    public async Task<VideoResponse> GetVideoByIdAsync(BuscarIdDTO buscar)
    {
        try
        {
            var video = await _videoRepository.GetVideo(Convert.ToInt16(buscar.Id));
            if (video == null)
            {
                return new VideoResponse
                {
                    StatusCode = 404,
                    Message = "Video with the provided ID does not exist."
                };
            }

            return new VideoResponse
            {
                StatusCode = 200,
                Message = "Video retrieved successfully.",
                Video = _mapper.Map<VideoDto>(video)
            };
        }
        catch (Exception ex)
        {
            return new VideoResponse
            {
                StatusCode = 500,
                Message = $"Error retrieving video.{ex.Message}"
            };
        }
    }

    public async Task<VideoResponse> InsertVideoAsync(VideoDto video)
    {
        try
        {
            
            await _videoRepository.Add(_mapper.Map<Video>(video));

            return new VideoResponse
            {
                StatusCode = 201,
                Message = "Video created successfully."
            };
        }
        catch (Exception ex)
        {
            return new VideoResponse
            {
                StatusCode = 500,
                Message = $"Error creating video with error {ex.Message}"
            };
        }
    }

    public async Task<VideoResponse> UpdateVideoAsync(int id, VideoDto video)
    {
        try
        {
            var existingVideo = await _videoRepository.GetVideo(id);
            if (existingVideo == null)
            {
                return new VideoResponse
                {
                    StatusCode = 404,
                    Message = "Video with the provided ID does not exist."
                };
            }

            existingVideo = _mapper.Map<Video>(video);
            await _videoRepository.Update(existingVideo);

            return new VideoResponse
            {
                StatusCode = 200,
                Message = "Video updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new VideoResponse
            {
                StatusCode = 500,
                Message = $"Error updating video with error {ex.Message}"
            };
        }
    }

    public async Task<VideoResponse> DeleteVideoAsync(BuscarIdDTO buscar)
    {
        try
        {
            var video = await _videoRepository.GetVideo(Convert.ToInt16(buscar.Id));

            if (video == null || video.IsDeleted)
            {
                return new VideoResponse
                {
                    StatusCode = 404,
                    Message = "Video with the provided ID does not exist or is already marked as deleted."
                };
            }

            video.IsDeleted = true;
            await _videoRepository.Update(video);

            return new VideoResponse
            {
                StatusCode = 200,
                Message = "Video deleted successfully (logically).",
            };
        }
        catch (Exception ex)
        {
            return new VideoResponse
            {
                StatusCode = 500,
                Message = $"Error deleting video with error {ex.Message}"
            };
        }
    }
}