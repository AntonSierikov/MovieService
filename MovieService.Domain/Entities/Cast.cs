namespace MovieService.Domain.Entities;

public class Cast
{
    public Guid Id { get; set; }

    public string Character { get; }

    public Guid ActorId { get; set; }

    public Guid MovieId { get; set; }

    public string ExtraId { get; }

    public Actor Actor { get; }

    public Movie Movie { get; }

    public Cast(
        Guid id,
        string character,
        string extraId,
        Guid actorId,
        Guid movieId,
        Actor actor,
        Movie movie)
    {
        Id = id;
        Character = character;
        ActorId = actorId;
        MovieId = movieId;
        ExtraId = extraId;
        Actor = actor;
        Movie = movie;
    }

    public static Cast Create(
        string character,
        string castExtraId,
        Guid actorId,
        Guid movieId,
        Actor actor,
        Movie movie) =>
        new Cast(Guid.NewGuid(), character, castExtraId, actorId, movieId, actor, movie);

    public static Cast Read(
        Guid id,
        string character,
        string castExtraId,
        Guid actorId,
        Guid movieId,
        Actor actor,
        Movie movie) =>
        new Cast(id, character, castExtraId, actorId, movieId, actor, movie);
}