using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.User;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.User;

public class GetAllUserDataUseCase : IGetAllUserDataUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public GetAllUserDataUseCase(IUserRepository userRepository, IMemoryCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }

    /// <summary>
    /// GetAllUserData
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<GetListUserResponse> GetAllUserData(long userId, GetListUserRequest request)
    {
        List<UserDto> userList = new();
        StatusEnum status = StatusEnum.Successed;
        try
        {
            var data = await _userRepository.GetAllData(request.KeyWord, request.PageIndex, request.PageSize);
            userList = data.Select(item => new UserDto(item)).ToList();
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
        return new GetListUserResponse(userId, userList, status, _cache);
    }
}
