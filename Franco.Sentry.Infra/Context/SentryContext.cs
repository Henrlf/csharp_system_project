using System.ComponentModel.DataAnnotations;
using Franco.Core.Dto.Messaging;
using Franco.Core.Infra.Extension;
using Franco.Sentry.Domain.Model;
using Franco.Sentry.Infra.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Franco.Sentry.Infra.Context;

public sealed class SentryContext : DbContext
{
    public DbSet<User> User {get; set;}
    
    private readonly IMediator _mediatorHandler;
    
    public SentryContext(DbContextOptions<SentryContext> options, IMediator mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;

        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(255)");
        }

        modelBuilder.ApplyConfiguration(new UserMap());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        var success = await SaveChangesAsync(cancellationToken) > 0;

        if (success)
        {
            await _mediatorHandler.PublishDomainEvents(this, cancellationToken);
        }

        return success;
    }
}