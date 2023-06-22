using MovieService.Domain.Entities;
using MovieService.Dto;

namespace MovieService.Converters;

public static class MovieConterters
{
    public static MovieDto ToDto(this Movie movie) =>
        new MovieDto()
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleasedAt = movie.ReleasedAt,
            BudgetAmount = movie.BudgetAmount,
            BudgetCurrency = movie.BudgetCurrency,
            Description = movie.Description,
            ProductionCompany = movie.ProductionCompany,
            Country = movie.Country,
            BoxOfficeAmount = movie.BoxOfficeAmount,
            BoxOfficeCurrency = movie.BoxOfficeCurrency,
            AverageMark = movie.AverageMark,
            VoteCount = movie.VoteCount,
            PosterUrl = movie.PosterUrl,
            Popularity = movie.Popularity,
            Runtime = movie.RunTime,
            Genres = movie.Genres.Select(g => g.GenreName).ToArray(),
            Casts = movie.Casts.Select(a => a.ToDto()).ToArray(),
        };

    public static CastDto ToDto(this Cast cast) =>
        new CastDto()
        {
            Id = cast.Id,
            Character = cast.Character,
            ActorDto = cast.Actor.ToDto(),
        };

    public static ActorDto ToDto(this Actor actor) =>
        new ActorDto()
        {
            Id = actor.Id,
            Name = actor.Name,
            Birthday = actor.Birthday,
            ProfilePhotoUrl = actor.ProfilePhotoUrl,
        };

    public static MovieReview ToDomain(this MovieReviewDto reviewDto, Guid movieId) => new MovieReview(
        reviewDto.Id,
        movieId,
        UserIdentity.UserId,
        reviewDto.Title,
        reviewDto.Content,
        false,
        true);

    public static MovieMark ToMarkDomain(this MovieReviewDto reviewDto, Guid movieId) =>
        new MovieMark(movieId, UserIdentity.UserId, reviewDto.Mark);

    public static MovieReviewDto ToDto(this MovieReview review, MovieMark movieMark) =>
        new MovieReviewDto()
        {
            Id = review.ReviewId,
            Title = review.Title,
            Content = review.ReviewText,
            Mark = movieMark.Mark,
        };
}