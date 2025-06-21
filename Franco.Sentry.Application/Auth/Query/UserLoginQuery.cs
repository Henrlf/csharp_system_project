using System.Text.Json.Serialization;
using Franco.Core.Dto.Messaging;
using MediatR;

namespace Franco.Sentry.Application.Auth.Query;

public class UserLoginQuery : IRequest<BaseResponse>
{
    [JsonPropertyName("usarname")]
    public string Username {get; set;} = string.Empty;

    [JsonPropertyName("password")]
    public string Password {get; set;} = string.Empty;
}