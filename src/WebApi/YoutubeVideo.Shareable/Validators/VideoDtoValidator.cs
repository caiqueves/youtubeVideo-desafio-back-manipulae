using FluentValidation;
using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.Shareable.Validators
{
    public class VideoDtoValidator : AbstractValidator<VideoDto>
    {
        public VideoDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(3, 100).WithMessage("Title should be between 3 and 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 500).WithMessage("Description should be between 10 and 500 characters.");

            RuleFor(x => x.ChannelTitle)
                .NotEmpty().WithMessage("Channel Title is required.");

            RuleFor(x => x.PublishedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");
        }
    }
}
