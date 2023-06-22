using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("movie_genres")]
public class MovieGenreDal
{
    [Column("movie_id")]
    public Guid MovieId { get; set; }

    [Column("genre_id")]
    public int GenreId { get; set; }

    public MovieDal Movie { get; set; }

    public GenreDal Genre { get; set; }
}