using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IGetUserByUserIdUseCase
{
    Task<GetUserByUserIdResponse> GetUserByUserId(long userId /* the user request this api */, GetUserByUserIdRequest request);
}
