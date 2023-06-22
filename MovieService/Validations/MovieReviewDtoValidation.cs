using FluentValidation;
using MovieService.Dto;

namespace MovieService.Validations;

public class MovieReviewDtoValidation : AbstractValidator<MovieReviewDto>
{
    public MovieReviewDtoValidation()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);
        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty()
            .MaximumLength(3000);
        RuleFor(x => x.Mark).ExclusiveBetween(0, 10);
    }
}