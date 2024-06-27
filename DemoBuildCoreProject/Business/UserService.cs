using HappyBookingServer.Interface;
using HappyBookingServer.Business.IService;
using HappyBookingShare.Response.User;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Model;
using HappyBookingShare.Common;

namespace HappyBookingServer.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// get all user data
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<GetListUserResponse> GetAllUserData(GetListUserRequest request)
    {
        List<UserDto> userList = new();
        StatusEnum status = StatusEnum.Successed;
        try
        {
            var keyword = request.KeyWord.Equals("_") ? string.Empty : request.KeyWord;
            var data = await _userRepository.GetAllData(keyword, request.PageIndex, request.PageSize);
            userList = data.Select(item => new UserDto(item)).ToList();
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
        return new GetListUserResponse(userList, status);
    }

    /// <summary>
    /// login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        StatusEnum status = StatusEnum.Successed;
        try
        {
            var user = await _userRepository.GetUserByLoginInfor(request.UserName, request.Password);
            if (user.UserId > 0)
            {
                var tokenResponse = await _tokenService.GenerateToken(user);
                token = tokenResponse.JwtToken;
                refeshToken = tokenResponse.RefreshToken;
            }
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }

        return new LoginResponse(token, refeshToken, status);
    }

    /// <summary>
    /// refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        StatusEnum status = StatusEnum.Successed;
        var result = await _tokenService.RefreshTokenAsync(request.JwtToken, request.RefreshToken);
        if (result != null)
        {
            token = result.JwtToken;
            refeshToken = result.RefreshToken;
        }

        return new LoginResponse(token, refeshToken, status);
    }

    /// <summary>
    /// register user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SaveUserResponse> RegisterUser(long userId, RegisterUserRequest request)
    {
        var userModel = new UserModel(
                            0,
                            request.FullName,
                            request.Email,
                            request.PhoneNumber,
                            request.CitizenIdentificationNumber,
                            request.Address,
                            request.AvatarImage,
                            request.Password);
        StatusEnum status = await ValidateUserInformation(userModel);
        if (status != StatusEnum.Successed)
        {
            return new SaveUserResponse(false, status);
        }
        var result = await _userRepository.SaveUser(userId, userModel);
        return new SaveUserResponse(result, status);
    }

    /// <summary>
    /// update user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SaveUserResponse> UpdateUser(long userId, UpdateUserRequest request)
    {
        var userModel = new UserModel(
                            request.UserId,
                            request.FullName,
                            request.Email,
                            request.PhoneNumber,
                            request.CitizenIdentificationNumber,
                            request.Address,
                            request.AvatarImage,
                            request.Password);
        StatusEnum status = await ValidateUserInformation(userModel);
        if (status != StatusEnum.Successed)
        {
            return new SaveUserResponse(false, status);
        }
        var result = await _userRepository.SaveUser(userId, userModel);
        return new SaveUserResponse(result, status);
    }

    #region private function
    /// <summary>
    /// validate user information
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    private async Task<StatusEnum> ValidateUserInformation(UserModel userModel)
    {
        bool isAddNewUser = true;
        if (userModel.UserId > 0)
        {
            isAddNewUser = false;
            var userExist = await _userRepository.GetUserByUserIdAndPassword(userModel.UserId, userModel.Password);
            if (userExist.UserId != userModel.UserId)
            {
                return StatusEnum.InvalidPassword;
            }
        }
        if (!string.IsNullOrEmpty(userModel.Email))
        {
            var userExist = await _userRepository.GetUserByEmail(userModel.Email);
            if (isAddNewUser && userExist.Email.Trim() == userModel.Email.Trim())
            {
                return StatusEnum.ExistEmail;
            }
            if (!isAddNewUser && userExist.UserId != userModel.UserId)
            {
                return StatusEnum.ExistEmail;
            }
        }
        if (!string.IsNullOrEmpty(userModel.PhoneNumber))
        {
            var userExist = await _userRepository.GetUserByPhone(userModel.PhoneNumber);
            if (isAddNewUser && userExist.PhoneNumber.Trim() == userModel.PhoneNumber.Trim())
            {
                return StatusEnum.ExistPhoneNumber;
            }
            if (!isAddNewUser && userExist.UserId != userModel.UserId)
            {
                return StatusEnum.ExistPhoneNumber;
            }
        }
        if (!string.IsNullOrEmpty(userModel.CitizenIdentificationNumber))
        {
            var userExist = await _userRepository.GetUserByCitizenIdentificationNumber(userModel.CitizenIdentificationNumber);
            if (isAddNewUser && userExist.CitizenIdentificationNumber.Trim() == userModel.CitizenIdentificationNumber.Trim())
            {
                return StatusEnum.ExistCitizenIdentificationNumber;
            }
            if (!isAddNewUser && userExist.UserId != userModel.UserId)
            {
                return StatusEnum.ExistCitizenIdentificationNumber;
            }
        }
        return StatusEnum.Successed;
    }

    #endregion 
}
