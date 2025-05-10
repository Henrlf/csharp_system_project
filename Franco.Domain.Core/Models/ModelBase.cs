using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Franco.Domain.Core.Enums;

namespace Franco.Domain.Core.Models;

public abstract class ModelBase
{
    [Key, Column("id")]
    public Guid Id {get; set;}

    [Column("status")]
    public StatusEnum? Status {get; set;} = StatusEnum.ENABLED;

    [Column("createdAt")]
    public DateTime CreatedAt {get; set;}

    // public IReadOnlyCollection<Event>? DomainEvents => _domainEvents?.AsReadOnly();
}