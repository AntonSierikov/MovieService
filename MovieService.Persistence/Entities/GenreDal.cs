using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("genres")]
public class GenreDal
{
    [Key]
    [Column("id")]
    public int GenreId { get; set; }

    [Column("genre")]
    public string Genre { get; set; }

    public IList<MovieGenreDal> MovieGeners { get; set; }
}