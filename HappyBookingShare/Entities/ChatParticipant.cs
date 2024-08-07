using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("ChatParticipant")]
public class ChatParticipant : BaseEntity
{
    [Key]
    public long Id { get; set; }

    public long ChatId { get; set; }

    public long MemberId { get; set; }
}
