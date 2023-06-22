using Microsoft.Extensions.Logging;
using MovieService.Domain.Entities;
using MovieService.Persistence;
using TmbdClient;
using TmbdClient.Entities;

namespace MovieService.ApplicationServices.Hangfire;

public class FeedTopMoviesBackgroundJob
{
    private readonly ILogger<FeedTopMoviesBackgroundJob> _logger;
    private readonly MovieAgent _movieAgent;
    private readonly IUnitOfWork _unitOfWork;

    public FeedTopMoviesBackgroundJob(
        ILogger<FeedTopMoviesBackgroundJob> logger,
        IUnitOfWork unitOfWork,
        MovieAgent movieAgent)
    {
        _logger = logger;
        _movieAgent = movieAgent;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            var state =
                await _unitOfWork.JobStateRepository.GetStateAsync<FeedTopMoviesState>(
                    nameof(FeedTopMoviesBackgroundJob));

            var page = state?.Page ?? 1;
            await SaveTopMoviePage(page);
            await _unitOfWork.JobStateRepository.UpsertAsync(
                nameof(FeedTopMoviesBackgroundJob),
                new FeedTopMoviesState() {Page = page + 1});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }

    //----------------------------------------------------------------//
    private async Task SaveTopMoviePage(int pageNumber)
    {
        var loadTopMovieIds = await _movieAgent.LoadTopMovieIdsByPageAsync(pageNumber);
        foreach (int movieId in loadTopMovieIds.Results.Select(r => r.Id))
        {
            await LoadMovieWithEntities(movieId);
        }
    }

    //----------------------------------------------------------------//

    public async Task LoadMovieWithEntities(int movieId)
    {
        var movie = await _unitOfWork.MovieRepository.GetMovieAsync(movieId);

        if (movie is not null)
            return;

        var movieDto = await _movieAgent.LoadMovieAsync(movieId.ToString());
        movie = ToDomain(movieDto);

        var transaction = _unitOfWork.StartTransaction();
        try
        {
            var genres = movieDto.Genres.Select(g => new Genre(g.Id, g.Name)).ToArray();
            await _unitOfWork.MovieRepository.UpsertGenresAsync(genres);
            await _unitOfWork.MovieRepository.UpsertMovieAsync(movie);
            await _unitOfWork.MovieRepository.UpsertMovieGenresAsync(genres.Select(g => (movie.Id, g.Id)).ToArray());
            var credits = await _movieAgent.LoadCreditsByMovieId(movieId);

            foreach (var cast in credits.Cast)
            {
                await SaveCast(movie, cast);
            }

            await transaction.CommitAsync();
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            _logger.LogError(exception.Message, exception);
        }
    }

    public async Task SaveCast(Movie movie, CastDto castDto)
    {
        var actor = await _unitOfWork.MovieRepository.GetActor(castDto.PeopleId);

        if (actor is null)
        {
            actor = Actor.Create(castDto.Name, DateTimeOffset.UtcNow, castDto.ProfilePath, castDto.PeopleId.ToString());
            await _unitOfWork.MovieRepository.InsertIfNotExists(actor);
        }

        Cast cast = Cast.Create(castDto.Character, castDto.CastId.ToString(), actor.Id, movie.Id, actor, movie);
        await _unitOfWork.MovieRepository.InsertIfNotExists(cast);
        await _unitOfWork.SaveChangesAsync();
    }

    private Movie ToDomain(MovieDto movieDto) =>
        Movie.Create(
            movieDto.Title,
            new DateTimeOffset(movieDto.ReleasedDate, TimeSpan.Zero),
            movieDto.Budget,
            "USD",
            movieDto.Overview,
            movieDto.ProductionCountries.FirstOrDefault()?.name ?? "Uknown Country",
            movieDto.ProductionCompanies.FirstOrDefault()?.Name ?? "Uknown Company",
            movieDto.Revenue,
            "USD",
            (decimal) movieDto.VoteAverage,
            movieDto.PosterPath,
            movieDto.VoteCount,
            movieDto.Id.ToString(),
            null,
            movieDto.Popularity,
            movieDto.Runtime ?? 0,
            Array.Empty<Genre>(),
            Array.Empty<Cast>());
}