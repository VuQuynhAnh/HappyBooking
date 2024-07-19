namespace HappyBookingShare.Entities;

public class BaseEntity
{
    /// <summary>
    /// 0 - IsDeleted = false
    /// 1 - IsDeleted = true
    /// </summary>
    public int IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public long CreatedId { get; set; }

    public DateTime UpdatedDate { get; set; }

    public long UpdatedId { get; set; }

}
