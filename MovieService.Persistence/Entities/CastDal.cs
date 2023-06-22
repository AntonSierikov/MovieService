using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("casts")]
public class CastDal
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("character")]
    public string Character { get; set; }

    [Column("extra_id")]
    public string ExtraId { get; set; }

    [Column("actor_id")]
    public Guid ActorId { get; set; }

    [Column("movie_id")]
    public Guid MovieId { get; set; }

    public ActorDal Actor { get; set; }

    public MovieDal Movie { get; set; }
}