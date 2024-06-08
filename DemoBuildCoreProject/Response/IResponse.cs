using DemoBuildCoreProject.Response.Status;

namespace DemoBuildCoreProject.Response;

public interface IResponse<T>
{
    T Data { get; }

    string Message { get; }

    CommonStatus Status { get; }
}
