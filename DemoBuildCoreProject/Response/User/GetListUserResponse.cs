using DemoBuildCoreProject.Response.Dtos;
using DemoBuildCoreProject.Response.Status;

namespace DemoBuildCoreProject.Response.User;

public class GetListUserResponse : IResponse<List<UserDto>>
{
    public GetListUserResponse(List<UserDto> data, string message, CommonStatus status)
    {
        Data = data;
        Message = message;
        Status = status;
    }

    public List<UserDto> Data { get; private set; }

    public string Message { get; private set; }

    public CommonStatus Status { get; private set; }
}
