using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("ImageManagement")]
public class ImageManagement : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public string ImageLink { get; set; } = string.Empty;

    /// <summary>
    /// 0 - not used
    /// 1 - used
    /// </summary>
    public int Status { get; set; }
}
