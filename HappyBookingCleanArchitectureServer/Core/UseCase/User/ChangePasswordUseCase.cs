using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.User;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public ChangePasswordUseCase(IUserRepository userRepository, IMemoryCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task<SaveUserResponse> ChangePassword(long userId, ChangePasswordRequest request)
    {
        try
        {
            if (userId > 0)
            {
                var userExist = await _userRepository.GetUserByUserIdAndPassword(userId, request.OldPassword);
                if (userExist.UserId != userId)
                {
                    return new SaveUserResponse(userId, false, StatusEnum.InvalidPassword, _cache);
                }
            }
            var result = await _userRepository.ChangePassword(userId, request.OldPassword, request.NewPassword);
            return new SaveUserResponse(userId, result, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
    }
}
