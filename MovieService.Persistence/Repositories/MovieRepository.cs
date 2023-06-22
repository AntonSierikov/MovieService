using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Entities;
using MovieService.Persistence.Converters;
using MovieService.Persistence.Entities;

namespace MovieService.Persistence.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieContext _movieContext;

    public MovieRepository(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    public async Task<Movie?> GetMovieAsync(Guid movieId)
    {
        var entity = await _movieContext.Movies
            .Include(m => m.Casts)
            .ThenInclude(m => m.Actor)
            .Include(m => m.Marks)
            .Include(m => m.Reviews)
            .Include(m => m.MovieGeners)
            .ThenInclude(m => m.Genre)
            .FirstOrDefaultAsync(m => m.Id == movieId);
        return entity?.ToDomain();
    }

    public async Task<Movie?> GetMovieAsync(int movieId)
    {
        var entity = await _movieContext.Movies
            .Include(m => m.Casts)
            .ThenInclude(m => m.Actor)
            .Include(m => m.Marks)
            .Include(m => m.Reviews)
            .Include(m => m.MovieGeners)
            .ThenInclude(m => m.Genre)
            .FirstOrDefaultAsync(m => m.MovieExtraId == movieId.ToString());
        return entity?.ToDomain();
    }

    public async Task<Actor?> GetActorAsync(Guid actorId)
    {
        var entity = await _movieContext.Actors.FirstOrDefaultAsync(m => m.Id == actorId);
        return entity?.ToDomain();
    }

    public async Task<Movie[]> GetMoviesAsync(
        string? movieName,
        SortOrder sortOrder,
        MovieSortStrategy sortStrategy,
        int limit,
        int offset)
    {
        Expression<Func<MovieDal, decimal>> sortExpression = sortStrategy switch
        {
            MovieSortStrategy.Popularity => m => m.Popularity,
            MovieSortStrategy.Rating => m => m.AverageMark,
            MovieSortStrategy.ReleasedAt => m => m.ReleasedAt.Ticks
        };
        var query = _movieContext.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(movieName))
            query = query.Where(m => EF.Functions.Like(m.Title, $"%{movieName}%"));
        
        query = sortOrder switch
            {
                SortOrder.Asc => query.OrderBy(sortExpression),
                SortOrder.Desc => query.OrderByDescending(sortExpression),
                _ => query,
            };
            
        query = query.Include(m => m.Casts)
            .ThenInclude(m => m.Actor)
            .Include(m => m.Marks)
            .Include(m => m.MovieGeners)
            .ThenInclude(m => m.Genre)
            .Include(m => m.Reviews);


        var entities = await query
            .Take(limit)
            .Skip(offset)
            .ToArrayAsync();
        return entities.Select(e => e.ToDomain()).ToArray();
    }

    public async Task<int> GetTotalAsync()
    {
        var count = await _movieContext.Movies.CountAsync();
        return count;
    }

    public Task UpsertMovieAsync(Movie movie)
    {
        return _movieContext.Movies.Upsert(movie.ToDal())
            .On(m => m.MovieExtraId)
            .WhenMatched((existingEntity, update) => new MovieDal()
            {
                Title = update.Title,
                ReleasedAt = update.ReleasedAt,
                BudgetAmount = update.BudgetAmount,
                BudgetCurrency = update.BudgetCurrency,
                Description = update.Description,
                ProductionCompany = update.ProductionCompany,
                Country = update.Country,
                AverageMark = update.AverageMark,
                PosterUrl = update.PosterUrl,
                BoxOfficeCurrency = update.BoxOfficeCurrency,
                BoxOfficeAmount = update.BoxOfficeAmount,
                VoteCount = movie.VoteCount,
                MovieExtraId = movie.MovieExtraId,
            })
            .RunAsync();
    }

    public Task UpsertGenresAsync(Genre[] genres)
    {
        return _movieContext.Genres.UpsertRange(genres.Select(g => new GenreDal()
        {
            GenreId = g.Id,
            Genre = g.GenreName,
        })).On(x => x.GenreId).RunAsync();
    }

    public Task UpsertMovieGenresAsync((Guid movieId, int genreId)[] movieGenres)
    {
        return _movieContext.MovieGenres
            .UpsertRange(movieGenres.Select(g => new MovieGenreDal()
        {
            GenreId = g.genreId,
            MovieId = g.movieId,
        })).On(x => new { x.MovieId, x.GenreId }).RunAsync();
    }

    public async Task<Actor?> GetActor(int actorId)
    {
        var actor = await _movieContext.Actors.FirstOrDefaultAsync(a => a.ExtraId == actorId.ToString());
        return actor?.ToDomain();
    }

    public async Task InsertIfNotExists(Cast cast)
    {
        if (await _movieContext.Casts.AnyAsync(c => c.Id == cast.Id || c.ExtraId == cast.ExtraId))
            return;

        _movieContext.Casts.Add(cast.ToDal());
    }

    public async Task InsertIfNotExists(Actor actor)
    {
        if (await _movieContext.Actors.AnyAsync(c => c.Id == actor.Id || c.ExtraId == actor.ExtraId))
            return;

        _movieContext.Actors.Add(actor.ToDal());
    }

    public Task UpsertMovieReviewAsync(MovieReview movieReview)
    {
        return _movieContext.MovieReviews.Upsert(movieReview.ToDal())
            .WhenMatched((existingEntity, update) => new MovieReviewDal()
            {
                Title = update.Title,
                ReviewText = update.ReviewText,
            })
            .RunAsync();
    }

    public Task UpsertMovieMarkAsync(MovieMark movieMark)
    {
        return _movieContext.MovieMarks.Upsert(movieMark.ToDal())
            .WhenMatched((existingEntity, update) => new MovieMarkDal()
            {
                Mark = update.Mark,
            })
            .RunAsync();
    }

    public async Task<(MovieReview Review, MovieMark Mark)[]> GetMovieReviewsAsync(Guid movieId)
    {
        var reviews = await _movieContext.MovieReviews.Where(r => r.MovieId == movieId).ToArrayAsync();
        var marks = await _movieContext.MovieMarks.Where(r => r.MovieId == movieId).ToArrayAsync();
        return reviews
            .Join(marks, r => r.MovieId, r => r.MovieId, (review, mark) => (Review: review.ToDomain(), Mark: mark.ToDomain()))
            .ToArray();
    }

    public async Task<(MovieReview? Review, MovieMark? Mark)> GetMovieReviewAsync(Guid reviewId)
    {
        var review =
            await _movieContext.MovieReviews.SingleOrDefaultAsync(m => m.ReviewId == reviewId);
        var mark = review is not null
            ? await _movieContext.MovieMarks.SingleOrDefaultAsync(m =>
                m.MovieId == review.MovieId && m.UserId == review.UserId)
            : null;
        return (review?.ToDomain(), mark?.ToDomain());
    }
}