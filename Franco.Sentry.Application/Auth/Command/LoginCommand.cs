using Franco.Core.Dto.Messaging;
using MediatR;

namespace Franco.Sentry.Application.Auth.Command;

public class LoginCommand : IRequest<Response> {}