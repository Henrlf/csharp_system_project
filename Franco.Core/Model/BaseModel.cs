// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
using Franco.Core.Dto.Messaging;

namespace Franco.Core.Model;

public abstract class BaseModel
{
    // [Key, Column("id")]
    public Guid Id {get; set;}

    // [Column("status")]
    public bool Status {get; set;} = true;

    // [Column("createdAt")]
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    
    public IReadOnlyCollection<Event>? DomainEvents => _domainEvents?.AsReadOnly();

    private List<Event>? _domainEvents;
    
    public void AddDomainEvent(Event domainEvent)
    {
        _domainEvents = _domainEvents ?? new List<Event>();
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(Event domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}