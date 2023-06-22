using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TmbdClient.Entities;

[DataContract]
public class ResponseWrapper<TResponse>
{
    [DataMember(Name = "response")]
    [JsonPropertyName("response")]
    public TResponse Response { get; set; }
}