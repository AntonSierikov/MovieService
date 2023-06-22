namespace MovieService.Domain.Entities;

public class Actor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public DateTimeOffset Birthday { get; private set; }

    public string ProfilePhotoUrl { get; private set; }

    public string ExtraId { get; set; }

    private Actor(Guid id, string name, DateTimeOffset birthday, string profilePhotoUrl, string extraId)
    {
        Id = id;
        Name = name;
        Birthday = birthday;
        ProfilePhotoUrl = profilePhotoUrl;
        ExtraId = extraId;
    }

    public static Actor Create(string name, DateTimeOffset birthday, string profilePhotoUrl, string extraId) =>
        new Actor(Guid.NewGuid(), name, birthday, profilePhotoUrl, extraId);

    public static Actor Read(Guid id, string name, DateTimeOffset birthday, string profilePhotoUrl, string extraId) =>
        new Actor(id, name, birthday, profilePhotoUrl, extraId);
}