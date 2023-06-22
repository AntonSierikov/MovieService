using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TmbdClient.Entities
{
    [DataContract]
    public class CastDto
    {
    
        [JsonPropertyName("cast_id")]
        public int CastId { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("movie_id")]
        public string MovieId { get; set; }

        [JsonPropertyName("id")]
        public int PeopleId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("popularity")]
        public decimal Popularity { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }        
    }
}
