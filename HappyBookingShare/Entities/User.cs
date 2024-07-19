using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("Users")]
public class User : BaseEntity
{
    [Key]
    public long UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string CitizenIdentificationNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string AvatarImage { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
