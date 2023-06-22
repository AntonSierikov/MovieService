using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("users")]
public class UserDal
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("login")]
    public string Login { get; set; }

    [Column("gender")]
    public int Gender { get; set; }

    [Column("country")]
    public string Country { get; set; }

    [Column("city")]
    public string City { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; }

    [Column("profile_photo")]
    public string ProfilePhoto { get; set; }

    [Column("registered_at")]
    public DateTimeOffset RegisteredAt { get; set; }

    public MovieMarkDal[] Marks { get; set; }

    public MovieReviewDal[] Reviews { get; set; }
}