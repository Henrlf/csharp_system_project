using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Franco.Core.Model;

namespace Franco.Sentry.Domain.Model;

public class User : BaseModel
{
    [Column("username"), MaxLength(255), Required]
    public string Username {get; set;} = null!;
    
    [Column("email"), MaxLength(255), Required]
    public required string Email {get; set;}

    [Column("password"), MaxLength(255), Required]
    public required string Password {get; set;}

    [Column("document"), MaxLength(20), Required]
    public required string Document {get; set;}

    [Column("phone"), MaxLength(25)]
    public string Phone {get; set;} = string.Empty;

}