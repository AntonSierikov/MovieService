using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("movies")]
public class MovieDal
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("released_at")]
    public DateTimeOffset ReleasedAt { get; set; }

    [Column("budget_amount")]
    public decimal BudgetAmount { get; set; }

    [Column("budget_currency")]
    public string BudgetCurrency { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("production_company")]
    public string ProductionCompany { get; set; }

    [Column("country")]
    public string Country { get; set; }

    [Column("box_office_amount")]
    public decimal BoxOfficeAmount { get; set; }

    [Column("box_office_currency")]
    public string BoxOfficeCurrency { get; set; }

    [Column("average_mark")]
    public decimal AverageMark { get; set; }

    [Column("vote_count")]
    public int VoteCount { get; set; }

    [Column("poster_url")]
    public string PosterUrl { get; set; }

    [Column("movie_extra_id")]
    public string MovieExtraId { get; set; }

    [Column("popularity")]
    public decimal Popularity { get; set; }

    [Column("runtime")]
    public int Runtime { get; set; }

    public IList<MovieMarkDal> Marks { get; set; }

    public IList<MovieReviewDal> Reviews { get; set; }

    public IList<MovieGenreDal> MovieGeners { get; set; }

    public IList<CastDal> Casts { get; set; }
}