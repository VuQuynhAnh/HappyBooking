using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("Setting")]
public class Setting : BaseEntity
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public string LanguageCode { get; set; } = string.Empty;
}
