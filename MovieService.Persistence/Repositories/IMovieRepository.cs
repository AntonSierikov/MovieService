using MovieService.Domain.Entities;

namespace MovieService.Persistence.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetMovieAsync(Guid movieId);

    Task<Movie?> GetMovieAsync(int movieId);

    Task<Actor?> GetActorAsync(Guid actorId);

    Task<Actor?> GetActor(int actorId);

    Task<Movie[]> GetMoviesAsync(
        string? movieName,
        SortOrder sortOrder,
        MovieSortStrategy sortStrategy,
        int limit,
        int offset);

    Task<int> GetTotalAsync();

    Task UpsertMovieAsync(Movie movie);

    Task InsertIfNotExists(Cast cast);

    Task InsertIfNotExists(Actor actor);

    Task UpsertGenresAsync(Genre[] genres);

    Task UpsertMovieGenresAsync((Guid movieId, int genreId)[] movieGenres);

    Task UpsertMovieReviewAsync(MovieReview movieReview);

    Task UpsertMovieMarkAsync(MovieMark movieMark);

    Task<(MovieReview Review, MovieMark Mark)[]> GetMovieReviewsAsync(Guid movieId);

    Task<(MovieReview? Review, MovieMark? Mark)> GetMovieReviewAsync(Guid reviewId);
}