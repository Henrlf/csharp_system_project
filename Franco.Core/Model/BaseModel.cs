using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Franco.Core.Model;

public abstract class BaseModel
{
    [Key, Column("id")]
    public Guid Id {get; set;}

    [Column("status")]
    public bool Status {get; set;} = true;

    [Column("createdAt")]
    public DateTime CreatedAt {get; set;} = DateTime.Now;
}