using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TmbdClient.Entities
{
    [DataContract]
    public class MovieDto
    {
        [DataMember(Name = "id")]
        [JsonPropertyName("id")]
        public int Id{ get; set; }

        [DataMember(Name = "adult")]
        [JsonPropertyName("adult")]
        public bool Adult{ get; set; }

        [DataMember(Name = "backdrop_path")]
        [JsonPropertyName("backdrop_path")]
        public string BackdropPath{ get; set; }

        [DataMember(Name = "budget")]
        [JsonPropertyName("budget")]
        public int Budget{ get; set; }

        [DataMember(Name = "original_language")]
        [JsonPropertyName("original_language")]
        public string OriginalLanguage{ get; set; }

        [DataMember(Name = "original_title")]
        [JsonPropertyName("original_title")]
        public string OriginalTitle{ get; set; }

        [DataMember(Name = "overview")]
        [JsonPropertyName("overview")]
        public string Overview{ get; set; }

        [DataMember(Name = "popularity")]
        [JsonPropertyName("popularity")]
        public decimal Popularity{ get; set; }

        [DataMember(Name = "poster_path")]
        [JsonPropertyName("poster_path")]
        public string PosterPath{ get; set; }

        [DataMember(Name = "release_date")]
        [JsonPropertyName("release_date")]
        public DateTime ReleasedDate{ get; set; }

        [DataMember(Name = "revenue")]
        [JsonPropertyName("revenue")]
        public decimal Revenue{ get; set; }

        [DataMember(Name = "runtime")]
        [JsonPropertyName("runtime")]
        public int? Runtime{ get; set; }

        [DataMember(Name = "status")]
        [JsonPropertyName("status")]
        public string Status{ get; set; }

        [DataMember(Name = "tagline")]
        [JsonPropertyName("tagline")]
        public string Tagline{ get; set; }

        [DataMember(Name = "title")]
        [JsonPropertyName("title")]
        public string Title{ get; set; }

        public bool Video{ get; set; }

        [DataMember(Name = "vote_average")]
        [JsonPropertyName("vote_average")]
        public double VoteAverage{ get; set; }

        [DataMember(Name = "vote_count")]
        [JsonPropertyName("vote_count")]
        public int VoteCount{ get; set; }

        [DataMember(Name = "genres")]
        [JsonPropertyName("genres")]
        public List<GenreDto> Genres{ get; set; }

        [DataMember(Name = "production_countries")]
        [JsonPropertyName("production_countries")]
        public List<ProductionCountryDto> ProductionCountries{ get; set; }

        [DataMember(Name = "production_companies")]
        [JsonPropertyName("production_companies")]
        public List<ProductionCompanyDto> ProductionCompanies{ get; set; }

    }
}
