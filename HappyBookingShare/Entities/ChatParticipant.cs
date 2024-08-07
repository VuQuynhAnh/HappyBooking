using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("ChatParticipant")]
public class ChatParticipant : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public int ChatId { get; set; }

    public int UserId { get; set; }
}
