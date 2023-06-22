using MovieService.Domain.Entities;
using MovieService.Persistence;

namespace MovieService.ApplicationServices;

public class MoviesService
{
    private readonly IUnitOfWork _unitOfWork;

    public MoviesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Movie?> GetMovieAsync(Guid movieId)
    {
        return _unitOfWork.MovieRepository.GetMovieAsync(movieId);
    }

    public async Task<(Movie[] Movies, int Total)> GetMoviesAsync(
        string? movieName,
        SortOrder sortOrder,
        MovieSortStrategy sortStrategy,
        int limit,
        int offset)
    {
        var movies = await _unitOfWork.MovieRepository.GetMoviesAsync(movieName, sortOrder, sortStrategy, limit, offset);
        var totalMovies = await _unitOfWork.MovieRepository.GetTotalAsync();
        return (movies, totalMovies);
    }

    public Task AddMovieReviewAsync(MovieReview movieReview)
    {
        return _unitOfWork.MovieRepository.UpsertMovieReviewAsync(movieReview);
    }

    public Task AddMovieMarkAsync(MovieMark movieMark)
    {
        return _unitOfWork.MovieRepository.UpsertMovieMarkAsync(movieMark);
    }

    public Task<(MovieReview Review, MovieMark Mark)[]> GetMovieReviewsAsync(Guid movieId)
    {
        return _unitOfWork.MovieRepository.GetMovieReviewsAsync(movieId);
    }

    public Task<(MovieReview? Review, MovieMark? Mark)> GetMovieReviewAsync(Guid reviewId)
    {
        return _unitOfWork.MovieRepository.GetMovieReviewAsync(reviewId);
    }
}