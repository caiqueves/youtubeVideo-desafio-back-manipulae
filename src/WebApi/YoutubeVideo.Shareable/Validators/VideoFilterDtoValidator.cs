using FluentValidation;
using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.Shareable.Validators
{
    public class VideoFilterDtoValidator : AbstractValidator<VideoFilterDto>
    {
        public VideoFilterDtoValidator()
        {
            // Validação do título
            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            // Validação da duração
            RuleFor(x => x.Duration)
                .MaximumLength(0).WithMessage("Duration must be greater than or equal to 0.");

            // Validação do autor
            RuleFor(x => x.Author)
                .MaximumLength(100).WithMessage("Author name cannot be longer than 100 characters.");

            // Validação da data de criação
            RuleFor(x => x.CreatedAfter)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAfter date cannot be in the future.");

            // Validação de pesquisa (Q)
            RuleFor(x => x.Q)
                .MaximumLength(100).WithMessage("Search query (Q) cannot be longer than 100 characters.");
                
        }
    }
}
