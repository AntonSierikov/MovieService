using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("actors")]
public class ActorDal
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("birthday")]
    public DateTimeOffset Birthday { get; set; }

    [Column("profile_photo_url")]
    public string? ProfilePhotoUrl { get; set; }

    [Column("extra_id")]
    public string ExtraId { get; set; } = null!;

    public IList<CastDal> Casts { get; set; }
}