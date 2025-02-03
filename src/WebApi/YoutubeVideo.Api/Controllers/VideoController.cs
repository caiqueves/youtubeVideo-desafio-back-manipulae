using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YoutubeVideo.Application.Interfaces;
using YoutubeVideo.Domain.Entities;
using YoutubeVideo.Shareable.DTOs;
using YoutubeVideo.Shareable.Validators;

namespace manipulaeHealth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IValidator<VideoDto> _videoDtoValidator;
    private readonly IValidator<VideoFilterDto> _videoFilterDtoValidator;
    private readonly IValidator<BuscarIdDTO> _videoBuscarDtoValidator;

    public VideoController(IVideoService videoService, IValidator<VideoDto> videoDtoValidator, IValidator<VideoFilterDto> videoFilterDtoValidator, IValidator<BuscarIdDTO> videoBuscarDtoValidator)
    {
        _videoService = videoService;
        _videoDtoValidator = videoDtoValidator;
        _videoFilterDtoValidator = videoFilterDtoValidator;
        _videoBuscarDtoValidator = videoBuscarDtoValidator;
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterVideos([FromBody] VideoFilterDto filterDto)
    {
       
        var videoFilterDtoValidator = new VideoFilterDtoValidator();
        // Chama o método Validate ou ValidateAsync e passa o objeto que queremos validar
        var result = videoFilterDtoValidator.Validate(filterDto);
        if (result.IsValid)
        {
            var response = await _videoService.FilterVideosAsync(filterDto);
            return Ok(response);
        }
        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        return BadRequest(errorMessages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVideoById(string id)
    {
        var buscar = new BuscarIdDTO { Id = id };
        var result = await _videoBuscarDtoValidator.ValidateAsync(buscar);

        if (result.IsValid)
        {
            var response = await _videoService.GetVideoByIdAsync(buscar);
            return Ok(response);
        }
        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        return BadRequest(errorMessages);
        
    }

    [HttpPost]
    public async Task<IActionResult> InsertVideo([FromBody] VideoDto video)
    {

        // Cria uma instância do validador
        var videoDtoValidator = new VideoDtoValidator();
        // Chama o método Validate ou ValidateAsync e passa o objeto que queremos validar
        var result = videoDtoValidator.Validate(video);
        if (result.IsValid)
        {
            var response = await _videoService.InsertVideoAsync(video);
            return StatusCode(response.StatusCode, response);
        }
        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        return BadRequest(errorMessages);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVideo(int id, [FromBody] VideoDto video)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID provided.");
        }

        var videoDtoValidator = new VideoDtoValidator();
        var result = await _videoDtoValidator.ValidateAsync(video);
        if (result.IsValid)
        {
            var response = await _videoService.UpdateVideoAsync(id, video);
            return StatusCode(response.StatusCode, response);
        }
        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
        return BadRequest(errorMessages);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo(string id)
    {
        var buscar = new BuscarIdDTO { Id = id };
        var validationResult = await _videoBuscarDtoValidator.ValidateAsync(buscar);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var response = await _videoService.DeleteVideoAsync(buscar);
        return StatusCode(response.StatusCode, response);
    }
}