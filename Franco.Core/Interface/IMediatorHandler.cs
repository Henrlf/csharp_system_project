using Franco.Core.Dto.Messaging;
using MediatR;

namespace Franco.Core.Interface;

public interface IMediatorHandler
{
    // Task PublishEvent<T>(T @event, CancellationToken cancellationToken) where T : Event;
    Task<TR> SendCommand<TR, T>(T command, CancellationToken cancellationToken) where TR : BaseResponse where T : IRequest<TR>;
    Task<TR> SendQuery<TR, T>(T query, CancellationToken cancellationToken) where TR : BaseResponse where T : IRequest<TR>;
}