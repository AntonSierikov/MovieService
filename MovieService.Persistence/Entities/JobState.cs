using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Persistence.Entities;

[Table("job_states")]
public class JobState
{
    [Key]
    [Column("job_id")]
    public string JobId { get; set; }

    [Column("state")]
    public string State { get; set; }
}