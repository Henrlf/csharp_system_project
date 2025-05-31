using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Franco.Core.Model;

namespace Franco.Sentry.Domain.Model;

public class User : ModelBase
{
    public int Identifier {get; set;} = 0;
    public string Key {get; set;} = null!;
    
    [Column("username"), MaxLength(255), Required]
    public string Username {get; set;} = null!;

    public User() {}
}