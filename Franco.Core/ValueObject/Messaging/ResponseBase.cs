using System.Text.Json.Serialization;
using Franco.Core.Enum;

namespace Franco.Core.ValueObject.Messaging;

public record ResponseBase()
{
    [JsonPropertyName("success")]
    public bool Success {get; set;} = false;

    [JsonPropertyName("code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HttpCodeEnum? Code {get; set;} = null;

    [JsonPropertyName("message")]
    public string Message {get; set;} = string.Empty;

    [JsonPropertyName("data")]
    public object Data {get; set;} = null!;
}