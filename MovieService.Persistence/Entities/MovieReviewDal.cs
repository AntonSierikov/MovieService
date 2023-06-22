using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("reviews")]
public class MovieReviewDal
{
    [Key]
    [Column("review_id")]
    public Guid ReviewId { get; set; }

    [Column("movie_id")]
    public Guid MovieId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("review_text")]
    public string ReviewText { get; set; }

    [Column("hidden")]
    public bool Hidden { get; set; }

    [Column("approved")]
    public bool Approved { get; set; }

    public MovieDal Movie { get; set; }
    
    public UserDal User { get; set; }
}