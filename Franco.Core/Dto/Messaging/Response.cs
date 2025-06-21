using System.Text.Json.Serialization;
using Franco.Core.Enum;

namespace Franco.Core.Dto.Messaging;

public record Response()
{
    [JsonPropertyName("success")]
    public bool Success {get; set;} = true;

    [JsonPropertyName("code")]
    public HttpCodeEnum? Code {get; set;} = HttpCodeEnum.SUCCESS;

    [JsonPropertyName("message")]
    public string Message {get; set;} = string.Empty;

    [JsonPropertyName("errors"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Errors { get; set; } = null;
    
    [JsonPropertyName("data")]
    public object Data {get; set;} = null!;
}