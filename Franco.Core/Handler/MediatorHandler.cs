using Franco.Core.Interface;
using Franco.Core.ValueObject.Messaging;
using MediatR;

namespace Franco.Core.Handler;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public Task<TR> SendCommand<TR, T>(T command, CancellationToken cancellationToken) where TR : ResponseBase where T : IRequest<TR>
    {
        return _mediator.Send(command, cancellationToken);
    }

    public async Task<TR> SendQuery<TR, T>(T query, CancellationToken cancellationToken) where TR : ResponseBase where T : IRequest<TR>
    {
        return await _mediator.Send(query, cancellationToken);
    }
}