using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBuildCoreProject.Entities;

[Table("Users")]
public class User : BaseEntity
{
    [Key]
    public long UserId { get; set; }

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public string Description { get; set; } = string.Empty;
}
