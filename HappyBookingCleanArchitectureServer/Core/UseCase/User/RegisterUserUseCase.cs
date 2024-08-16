using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Model;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.User;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMemoryCache _cache;

    public RegisterUserUseCase(IUserRepository userRepository, IMemoryCache cache, IImageRepository imageRepository)
    {
        _userRepository = userRepository;
        _cache = cache;
        _imageRepository = imageRepository;
    }

    /// <summary>
    /// RegisterUser
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SaveUserResponse> RegisterUser(long userId, RegisterUserRequest request)
    {
        try
        {
            var userModel = new UserModel(
                            0,
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
                return new SaveUserResponse(0, false, status, _cache);
            }
            var result = await _userRepository.SaveUser(userId, userModel);
            await _imageRepository.UsedImage(userModel.AvatarImage, userId);
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
            if (userExist.Email.Trim() == userModel.Email.Trim())
            {
                return StatusEnum.ExistEmail;
            }
        }
        if (!string.IsNullOrEmpty(userModel.PhoneNumber))
        {
            var userExist = await _userRepository.GetUserByPhone(userModel.PhoneNumber);
            if (userExist.PhoneNumber.Trim() == userModel.PhoneNumber.Trim())
            {
                return StatusEnum.ExistPhoneNumber;
            }
        }
        if (!string.IsNullOrEmpty(userModel.CitizenIdentificationNumber))
        {
            var userExist = await _userRepository.GetUserByCitizenIdentificationNumber(userModel.CitizenIdentificationNumber);
            if (userExist.CitizenIdentificationNumber.Trim() == userModel.CitizenIdentificationNumber.Trim())
            {
                return StatusEnum.ExistCitizenIdentificationNumber;
            }
        }
        return StatusEnum.Successed;
    }
    #endregion 
}
