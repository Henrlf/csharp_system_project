using System.Text.Json.Serialization;

namespace Franco.Core.ValueObject.Auth;

public class AuthJwt
{
    [JsonPropertyName("UserId")]
    public int UserId {get; set;} = 0;

    [JsonPropertyName("Name")]
    public string Name {get; set;} = string.Empty;

    [JsonPropertyName("hours")]
    public double Hours {get; set;} = 4;
}