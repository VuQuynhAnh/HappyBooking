﻿using HappyBookingShare.Request.Auth;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Auth;
using HappyBookingShare.Response.User;

namespace HappyBookingServer.Business.IService;

public interface IUserService
{
    Task<GetListUserResponse> GetAllUserData(long userId, GetListUserRequest request);

    Task<LoginResponse> Login(LoginRequest request);

    Task<LoginResponse> RefreshToken(RefreshTokenRequest request);

    Task<SaveUserResponse> RegisterUser(long userId, RegisterUserRequest request);

    Task<SaveUserResponse> UpdateUser(long userId, UpdateUserRequest request);
}
