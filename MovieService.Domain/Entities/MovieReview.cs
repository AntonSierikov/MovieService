namespace MovieService.Domain.Entities;

public class MovieReview
{
    public Guid ReviewId { get; private set; }

    public Guid MovieId { get; private set; }

    public Guid UserId { get; private set; }

    public string Title { get; private set; }

    public string ReviewText { get; private set; }

    public bool Hidden { get; private set; }

    public bool Approved { get; private set; }

    public MovieReview(
        Guid reviewId,
        Guid movieId,
        Guid userId,
        string title,
        string reviewText,
        bool hidden,
        bool approved)
    {
        ReviewId = reviewId;
        MovieId = movieId;
        UserId = userId;
        Hidden = hidden;
        Approved = approved;
        ReviewText = reviewText;
        Title = title;
    }

    public static MovieReview Create(Movie movie, User user, string title, string reviewText, bool hidden)
    {
        return new MovieReview(
            Guid.NewGuid(),
            movie.Id,
            user.Id,
            title,
            reviewText,
            hidden,
            false);
    }

    public static MovieReview Read(
        Guid reviewId,
        Guid movieId,
        Guid userId,
        string title,
        string reviewText,
        bool hidden,
        bool approved) =>
        new MovieReview(
            reviewId,
            movieId,
            userId,
            title,
            reviewText,
            hidden,
            approved);

    public void Update(string title, string reviewText)
    {
        Title = title;
        ReviewText = reviewText;
    }
}