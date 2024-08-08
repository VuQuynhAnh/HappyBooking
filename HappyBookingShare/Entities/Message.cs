using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("Message")]
public class Message : BaseEntity
{
    [Key]
    public long MessageId { get; set; }

    public long ChatId { get; set; }

    public int MessageType { get; set; }

    public string Content { get; set; } = string.Empty;
}
