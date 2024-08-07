using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("Message")]
public class Message : BaseEntity
{
    [Key]
    public int MessageId { get; set; }

    public int ChatId { get; set; }

    public int UserId { get; set; }

    public int TypeMessage { get; set; }

    public string Content { get; set; } = string.Empty;
}
