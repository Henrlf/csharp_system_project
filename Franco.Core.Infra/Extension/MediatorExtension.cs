using Franco.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Franco.Core.Infra.Extension;

public static class MediatorExtension
{
    public static async Task PublishDomainEvents<T>(this IMediator mediator, T ctx, CancellationToken cancellationToken) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<BaseModel>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Count != 0);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents.Select(async (domainEvent) =>
        {
            await mediator.Publish(domainEvent, cancellationToken);
        });

        await Task.WhenAll(tasks);
    }
}