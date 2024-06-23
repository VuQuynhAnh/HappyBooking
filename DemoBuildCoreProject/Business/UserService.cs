using HappyBookingServer.Interface;
using HappyBookingServer.Business.IService;
using HappyBookingShare.Response.User;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Status;
using HappyBookingShare.Response.Dtos;

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

    public async Task<GetListUserResponse> GetAllUserData(GetListUserRequest request)
    {
        List<UserDto> userList = new();
        string message = string.Empty;
        CommonStatus status = CommonStatus.Successed;
        try
        {
            var data = await _userRepository.GetAllData(request.KeyWord, request.PageIndex, request.PageSize);
            userList = data.Select(item => new UserDto(item)).ToList();
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
        return new GetListUserResponse(userList, message, status);
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        string message = string.Empty;
        CommonStatus status = CommonStatus.Successed;
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

        return new LoginResponse(token, refeshToken, message, status);
    }

    public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        string message = string.Empty;
        CommonStatus status = CommonStatus.Successed;
        var result = await _tokenService.RefreshTokenAsync(request.JwtToken, request.RefreshToken);
        if (result != null)
        {
            token = result.JwtToken;
            refeshToken = result.RefreshToken;
        }

        return new LoginResponse(token, refeshToken, message, status);
    }
}
