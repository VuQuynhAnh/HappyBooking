using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBuildCoreProject.Entities;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Password { get; set; }

    public string Description { get; set; } = string.Empty;
}
