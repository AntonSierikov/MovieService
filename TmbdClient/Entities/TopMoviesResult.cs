using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TmbdClient.Entities;

[DataContract]
public class TopMoviesResult
{
    [DataMember(Name = "page")]
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [DataMember(Name = "results")]
    [JsonPropertyName("results")]
    public TopMovieItem[] Results { get; set; }

    [DataMember(Name = "total_pages")]
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [DataMember(Name = "total_results")]
    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}

[DataContract]
public class TopMovieItem
{
    [DataMember(Name = "id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }
}