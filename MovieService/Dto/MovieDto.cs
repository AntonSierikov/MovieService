using System.Runtime.Serialization;
using MovieService.Domain.Entities;

namespace MovieService.Dto;

[DataContract]
public class MovieDto
{
    [DataMember(Name ="id")]
    public Guid Id { get; set; }

    [DataMember(Name ="title")]
    public string Title { get; set; }

    [DataMember(Name ="released_at")]
    public DateTimeOffset ReleasedAt { get; set; }

    [DataMember(Name ="budget_amount")]
    public decimal BudgetAmount { get; set; }

    [DataMember(Name ="budget_currency")]
    public string BudgetCurrency { get; set; }

    [DataMember(Name ="description")]
    public string Description { get; set; }

    [DataMember(Name ="production_company")]
    public string ProductionCompany { get; set; }

    [DataMember(Name ="production_country")]
    public string Country { get; set; }

    [DataMember(Name ="box_office_amount")]
    public decimal BoxOfficeAmount { get; set; }

    [DataMember(Name ="box_office_currency")]
    public string BoxOfficeCurrency { get; set; }

    [DataMember(Name ="average_mark")]
    public decimal AverageMark { get; set; }

    [DataMember(Name = "vote_count")]
    public int VoteCount { get; set; }

    [DataMember(Name ="poster_url")]
    public string PosterUrl { get; set; }

    [DataMember(Name = "popularity")]
    public decimal Popularity { get; set; }

    [DataMember(Name = "runtime")]
    public int Runtime { get; set; }

    [DataMember(Name = "genres")]
    public string[] Genres { get; set; }

    [DataMember(Name = "director_id")]
    public Guid DirectorId { get; set; }

    public ActorDto Director { get; set; }
    public CastDto[] Casts { get; set; }
}

[DataContract]
public class CastDto
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }

    [DataMember(Name = "character")]
    public string Character { get; set; }

    [DataMember(Name = "actor")]
    public ActorDto ActorDto { get; set; }
}

public class ActorDto
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "birthday")]
    public DateTimeOffset Birthday { get; set; }

    [DataMember(Name = "profile_photo_url")]
    public string ProfilePhotoUrl { get; set; }
}