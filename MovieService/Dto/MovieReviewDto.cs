using System.Runtime.Serialization;

namespace MovieService.Dto;

[DataContract]
public class MovieReviewDto
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "content")]
    public string Content { get; set; }

    [DataMember(Name = "mark")]
    public int Mark { get; set; }
}