using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IGetAllUserDataUseCase
{
    Task<GetListUserResponse> GetAllUserData(long userId, GetListUserRequest request);
}
