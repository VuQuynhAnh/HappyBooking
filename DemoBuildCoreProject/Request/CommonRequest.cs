namespace DemoBuildCoreProject.Request;

public class CommonRequest
{
    public string KeyWord { get; set; } = string.Empty;

    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
