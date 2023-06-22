namespace MovieService.Domain.Entities;

public class MovieMark
{
    public Guid MovieId { get; private set; }

    public Guid UserId { get; private set; }

    public int Mark { get; private set; }

    public MovieMark(
        Guid movieId,
        Guid userId,
        int mark)
    {
        MovieId = movieId;
        UserId = userId;
        Mark = mark;
    }
    
    public static MovieMark Create(Movie movie, User user, int mark)
    {
        return new MovieMark(movie.Id, user.Id, mark);
    }

    public void UpdateMark(int mark)
    {
        Mark = mark;
    }
}