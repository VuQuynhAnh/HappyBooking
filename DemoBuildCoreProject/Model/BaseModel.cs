namespace DemoBuildCoreProject.Model;

public class BaseModel
{
    public int IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public long CreatedId { get; set; }

    public DateTime UpdatedDate { get; set; }

    public long UpdatedId { get; set; }
}
