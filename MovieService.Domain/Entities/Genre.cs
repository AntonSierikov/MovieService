namespace MovieService.Domain.Entities;

public class Genre
{
    public int Id { get; set; }

    public string GenreName { get; set; }

    public Genre(int id, string name)
    {
        Id = id;
        GenreName = name;
    }
}