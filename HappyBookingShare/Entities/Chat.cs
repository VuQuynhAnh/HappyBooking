using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("Chat")]
public class Chat : BaseEntity
{
    [Key]
    public int ChatId { get; set; }

    public string ChatName { get; set; } = string.Empty;

    public bool IsGroupChat { get; set; }

    public string GroupAvatar { get; set; } = string.Empty;
}
