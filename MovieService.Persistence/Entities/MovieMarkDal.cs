using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("movie_marks")]
public class MovieMarkDal
{
    [Column("movie_id")]
    public Guid MovieId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("mark")]
    public int Mark { get; set; }

    public MovieDal Movie { get; set; }

    public UserDal User { get; set; }
}