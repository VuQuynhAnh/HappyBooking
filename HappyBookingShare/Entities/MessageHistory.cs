using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HappyBookingShare.Entities;

[Table("MessageHistory")]
public class MessageHistory
{
    [Key]
    public long HistoryId { get; set; }

    public long MessageId { get; set; }

    public long ChatId { get; set; }

    public int MessageType { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public long CreatedId { get; set; }
}
