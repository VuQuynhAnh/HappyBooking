using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Model;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.User;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMemoryCache _cache;

    public UpdateUserUseCase(IUserRepository userRepository, IMemoryCache cache, IImageRepository imageRepository)
    {
        _userRepository = userRepository;
        _cache = cache;
        _imageRepository = imageRepository;
    }

    /// <summary>
    /// UpdateUser
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SaveUserResponse> UpdateUser(long userId, UpdateUserRequest request)
    {
        try
        {
            var userModel = new UserModel(
                           request.UserId,
                           request.FullName,
                           request.Email,
                           request.PhoneNumber,
                           request.CitizenIdentificationNumber,
                           request.Address,
                           request.AvatarImage,
                           request.Role,
                           request.Password,
                           false);
            StatusEnum status = await ValidateUserInformation(userModel);
            if (status != StatusEnum.Successed)
            {
                return new SaveUserResponse(userId, false, status, _cache);
            }
            var result = await _userRepository.SaveUser(userId, userModel);
            return new SaveUserResponse(userId, result, status, _cache);
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
    }

    #region private function
    /// <summary>
    /// validate user information
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    private async Task<StatusEnum> ValidateUserInformation(UserModel userModel)
    {
        if (!string.IsNullOrEmpty(userModel.Email))
        {
            var userExist = await _userRepository.GetUserByEmail(userModel.Email);
            if (userExist.UserId != 0 && userExist.UserId != userModel.UserId)
            {
                return StatusEnum.ExistEmail;
            }
        }
        if (!string.IsNullOrEmpty(userModel.PhoneNumber))
        {
            var userExist = await _userRepository.GetUserByPhone(userModel.PhoneNumber);
            if (userExist.UserId != 0 && userExist.UserId != userModel.UserId)
            {
                return StatusEnum.ExistPhoneNumber;
            }
        }
        if (!string.IsNullOrEmpty(userModel.CitizenIdentificationNumber))
        {
            var userExist = await _userRepository.GetUserByCitizenIdentificationNumber(userModel.CitizenIdentificationNumber);
            if (userExist.UserId != 0 && userExist.UserId != userModel.UserId)
            {
                return StatusEnum.ExistCitizenIdentificationNumber;
            }
        }
        return StatusEnum.Successed;
    }
    #endregion
}
