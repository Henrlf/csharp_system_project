using System.Text.Json.Serialization;

namespace Franco.Sentry.Application.Auth.Dto;

public class AuthJwt
{
    [JsonPropertyName("idUser")]
    public Guid IdUser {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;} = string.Empty;

    [JsonPropertyName("hours")]
    public double Hours {get; set;} = 4;

    [JsonIgnore] 
    public bool IsAdmin { get; set; } = false;
}