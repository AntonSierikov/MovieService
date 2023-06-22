using MovieService.Domain.Entities;
using MovieService.Persistence.Entities;

namespace MovieService.Persistence.Converters;

public static class MovieConterter
{
    public static MovieDal ToDal(this Movie movie) =>
        new MovieDal()
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
            MovieExtraId = movie.MovieExtraId,
            Popularity = movie.Popularity,
            Runtime = movie.RunTime,
        };

    public static Movie ToDomain(this MovieDal movie) => Movie.Read(
        movie.Id,
        movie.Title,
        movie.ReleasedAt,
        movie.BudgetAmount,
        movie.BudgetCurrency,
        movie.Description,
        movie.Country,
        movie.ProductionCompany,
        movie.BoxOfficeAmount,
        movie.BoxOfficeCurrency,
        movie.AverageMark,
        movie.PosterUrl,
        movie.VoteCount,
        movie.MovieExtraId,
        null,
        movie.Popularity,
        movie.Runtime,
        movie.MovieGeners.Select(g => new Genre(g.GenreId, g.Genre.Genre)).ToArray(),
        movie.Casts.Select(cast => Cast.Read(cast.Id, cast.Character, cast.ExtraId, cast.ActorId, cast.MovieId, cast.Actor.ToDomain(), null)).ToArray());

    public static ActorDal ToDal(this Actor actor) =>
        new ActorDal()
        {
            Id = actor.Id,
            Name = actor.Name,
            Birthday = actor.Birthday,
            ProfilePhotoUrl = actor.ProfilePhotoUrl,
            ExtraId = actor.ExtraId,
        };

    public static CastDal ToDal(this Cast cast) =>
        new CastDal()
        {
            Id = cast.Id,
            Character = cast.Character,
            ExtraId = cast.ExtraId,
            ActorId = cast.ActorId,
            MovieId = cast.MovieId,
        };

    public static Cast ToDomain(this CastDal cast) =>
        Cast.Read(cast.Id, cast.Character, cast.ExtraId, cast.ActorId, cast.MovieId, cast.Actor?.ToDomain(), cast.Movie?.ToDomain());

    public static Actor ToDomain(this ActorDal actor) => Actor.Read(actor.Id, actor.Name, actor.Birthday, actor.ProfilePhotoUrl, actor.ExtraId);

    public static MovieReviewDal ToDal(this MovieReview review) =>
        new MovieReviewDal()
        {
            ReviewId = review.ReviewId,
            MovieId = review.MovieId,
            UserId = review.UserId,
            Title = review.Title,
            ReviewText = review.ReviewText,
            Hidden = review.Hidden,
            Approved = review.Approved,
        };

    public static MovieReview ToDomain(this MovieReviewDal review) =>
        MovieReview.Read(
            review.ReviewId,
            review.MovieId,
            review.UserId,
            review.Title,
            review.ReviewText,
            review.Hidden,
            review.Approved);

    public static MovieMarkDal ToDal(this MovieMark mark) =>
        new MovieMarkDal()
        {
            UserId = mark.UserId,
            MovieId = mark.MovieId,
            Mark = mark.Mark,
        };

    public static MovieMark ToDomain(this MovieMarkDal mark) => new MovieMark(mark.MovieId, mark.UserId, mark.Mark);
}