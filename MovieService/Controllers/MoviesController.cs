using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieService.ApplicationServices;
using MovieService.Converters;
using MovieService.Domain.Entities;
using MovieService.Dto;

namespace MovieService.Controllers;

[Route("api/v1/movies/")]
public class MoviesController
{
    private readonly MoviesService _moviesService;

    public MoviesController(MoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet("{movieId:guid}")]
    public async Task<IActionResult> GetMovieAsync([FromRoute] Guid movieId)
    {
        var movie = await _moviesService.GetMovieAsync(movieId);

        if (movie is null)
            return new NotFoundResult();

        return new OkObjectResult(movie.ToDto());
    }

    [HttpPut]
    public Task<IActionResult> UpsertMovieAsync([FromBody] MovieDto movieDto)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetMoviesAsync(
        string? movieName,
        [FromQuery(Name = "sort_order")] SortOrder sortOrder,
        [FromQuery(Name = "sort_strategy")] MovieSortStrategy sortStrategy,
        [FromQuery(Name = "limit")] int limit,
        [FromQuery(Name = "offset")] int offset)
    {
        var (movies, total) = await _moviesService.GetMoviesAsync(movieName, sortOrder, sortStrategy, limit, offset);
        return new OkObjectResult(
            new MovieListDto()
            {
                Items = movies.Select(x => x.ToDto()).ToArray(),
                Total = total,
            });
    }

    [HttpGet("{movieId:guid}/reviews")]
    public async Task<IActionResult> GetMovieReviewAsync([FromRoute] Guid movieId)
    {
        var reviews = await _moviesService.GetMovieReviewsAsync(movieId);
        return new OkObjectResult(reviews.Select(r => r.Review.ToDto(r.Mark)).ToArray());

    }

    [HttpGet("{movieId:guid}/reviews/{reviewId:guid}")]
    public async Task<IActionResult> GetMovieReviewAsync([FromRoute] Guid movieId, [FromRoute] Guid reviewId)
    {
        var review = await _moviesService.GetMovieReviewAsync(reviewId);
        if (review.Review is null || review.Mark is null)
            return new NotFoundResult();

        return new ObjectResult(review.Review.ToDto(review.Mark));
    }

    [HttpPut("{movieId:guid}/reviews")]
    public async Task<IActionResult> CreateMovieReviewsAsync(
        [FromRoute] Guid movieId,
        [FromBody]MovieReviewDto reviewDto,
        [FromServices] IValidator<MovieReviewDto> validator)
    {
        var validationResult = await validator.ValidateAsync(reviewDto);

        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult.Errors);
        }

        var review = reviewDto.ToDomain(movieId);
        await _moviesService.AddMovieReviewAsync(review);
        await _moviesService.AddMovieMarkAsync(reviewDto.ToMarkDomain(movieId));
        return new OkResult();
    }
}