using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.User;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.User;

public class GetUserByUserIdUseCase : IGetUserByUserIdUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public GetUserByUserIdUseCase(IUserRepository userRepository, IMemoryCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }

    /// <summary>
    /// GetUserByUserId
    /// </summary>
    /// <param name="userId">the user request this api</param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<GetUserByUserIdResponse> GetUserByUserId(long userId /* the user request this api */, GetUserByUserIdRequest request)
    {
        StatusEnum status = StatusEnum.Successed;
        UserDto userDto;
        try
        {
            var data = await _userRepository.GetUserByUserId(request.UserId);
            userDto = new UserDto(data);
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
        return new GetUserByUserIdResponse(userId, userDto, status, _cache);
    }
}
