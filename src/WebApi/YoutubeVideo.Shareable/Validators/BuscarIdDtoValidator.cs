using FluentValidation;
using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.Shareable.Validators
{
    public class BuscarIdDtoValidator : AbstractValidator<BuscarIdDTO>
    {
        public BuscarIdDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
