using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Database;
using HappyBookingShare.Entities;
using HappyBookingShare.Model;
using Microsoft.EntityFrameworkCore;

namespace HappyBookingCleanArchitectureServer.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// GetAllData
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<UserModel>> GetAllData(string keyword, int pageIndex, int pageSize)
    {
        keyword = keyword.ToLower();
        var result = await _context.UserRepository.Where(item => (item.FullName.ToLower().Contains(keyword)
                                                                  || item.Address.ToLower().Contains(keyword)
                                                                  || item.Email.ToLower().Contains(keyword)
                                                                  || item.PhoneNumber.Contains(keyword)
                                                                  || item.CitizenIdentificationNumber.Contains(keyword))
                                                                 && item.IsDeleted == 0)
                                                  .Skip((pageIndex - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .Select(item => new UserModel(item))
                                                  .ToListAsync();
        return result;
    }

    /// <summary>
    /// GetUserByUserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<UserModel> GetUserByUserId(long userId)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserId == userId
                                                                                 && item.IsDeleted == 0);
        if (userItem == null)
        {
            return new();
        }
        return new UserModel(userItem);
    }

    /// <summary>
    /// GetUserByEmail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<UserModel> GetUserByEmail(string email)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.Email == email
                                                                                 && item.IsDeleted == 0);
        if (userItem == null)
        {
            return new();
        }
        return new UserModel(userItem);
    }

    /// <summary>
    /// GetUserByPhone
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public async Task<UserModel> GetUserByPhone(string phoneNumber)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.PhoneNumber == phoneNumber
                                                                                 && item.IsDeleted == 0);
        if (userItem == null)
        {
            return new();
        }
        return new UserModel(userItem);
    }

    /// <summary>
    /// GetUserByCitizenIdentificationNumber
    /// </summary>
    /// <param name="citizenIdentificationNumber"></param>
    /// <returns></returns>
    public async Task<UserModel> GetUserByCitizenIdentificationNumber(string citizenIdentificationNumber)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.CitizenIdentificationNumber == citizenIdentificationNumber
                                                                                 && item.IsDeleted == 0);
        if (userItem == null)
        {
            return new();
        }
        return new UserModel(userItem);
    }

    /// <summary>
    /// GetUserByUserIdAndPassword
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<UserModel> GetUserByUserIdAndPassword(long userId, string password)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.Password == password
                                                                                 && item.IsDeleted == 0
                                                                                 && item.UserId == userId);
        if (userItem == null)
        {
            return new();
        }
        return new UserModel(userItem);
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<UserModel> Login(string userName, string password)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.Password == password
                                                                                 && item.IsDeleted == 0
                                                                                 && (item.CitizenIdentificationNumber == userName
                                                                                     || item.PhoneNumber == userName
                                                                                     || item.Email == userName));
        if (userItem == null)
        {
            return new();
        }
        userItem.IsOnline = true;
        userItem.LastHeartbeatTime = DateTime.UtcNow;
        userItem.LastLoginTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return new UserModel(userItem);
    }

    /// <summary>
    /// SaveUser
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userModel"></param>
    /// <returns></returns>
    public async Task<bool> SaveUser(long userId, UserModel userModel)
    {
        var entity = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserId == userModel.UserId
                                                                               && item.IsDeleted == 0);

        if (entity == null)
        {
            entity = new();
            entity.UserId = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedId = userId;
            entity.Password = userModel.Password;
        }
        entity.FullName = userModel.FullName;
        entity.PhoneNumber = userModel.PhoneNumber;
        entity.Email = userModel.Email;
        entity.CitizenIdentificationNumber = userModel.CitizenIdentificationNumber;
        entity.Address = userModel.Address;
        entity.AvatarImage = userModel.AvatarImage;
        entity.Role = userModel.Role;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedId = userId;
        if (entity.UserId == 0)
        {
            _context.UserRepository.Add(entity);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// UpdatePassword
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public async Task<bool> ChangePassword(long userId, string oldPassword, string newPassword)
    {
        var entity = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserId == userId
                                                                               && item.Password == oldPassword
                                                                               && item.IsDeleted == 0);

        if (entity == null)
        {
            return false;
        }
        entity.Password = newPassword;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedId = userId;
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// GenerateRefreshToken
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<string> GenerateRefreshToken(UserModel user)
    {
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.UserId,
            ExpiryDate = DateTime.UtcNow.AddDays(1),
            IsRevoked = false
        };

        await _context.RefreshTokenRepository.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken.Token;
    }

    public async Task ReleaseResource()
    {
        await _context.DisposeAsync();
    }

    /// <summary>
    /// CheckExistUserList
    /// </summary>
    /// <param name="userIdList"></param>
    /// <returns></returns>
    public async Task<bool> CheckExistUserList(List<long> userIdList)
    {
        userIdList = userIdList.Distinct().ToList();
        var userIdExist = await _context.UserRepository.CountAsync(item => userIdList.Contains(item.UserId) && item.IsDeleted == 0);
        return userIdList.Count() == userIdExist;
    }

    /// <summary>
    /// HeartbeatUser
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<UserModel> HeartbeatUser(long userId)
    {
        var user = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserId == userId && item.IsDeleted == 0);
        if (user == null)
        {
            return new();
        }
        user.IsOnline = true;
        user.LastHeartbeatTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return new UserModel(user);
    }

    /// <summary>
    /// MarkUserAsOffline
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> MarkUserAsOffline(long userId)
    {
        var user = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserId == userId && item.IsDeleted == 0);
        if (user == null)
        {
            return false;
        }
        user.IsOnline = false;
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// AutoMarkUserAsOffline
    /// </summary>
    /// <param name="lastSecond"></param>
    /// <returns></returns>
    public async Task<List<long>> AutoMarkUserAsOffline(int lastSecond)
    {
        var userList = await _context.UserRepository.Where(item => item.IsDeleted == 0
                                                                   && item.IsOnline
                                                                   && (DateTime.UtcNow - item.LastHeartbeatTime).TotalSeconds >= lastSecond)
                                                    .ToListAsync();
        List<long> userIdList = new();
        foreach (var item in userList)
        {
            item.IsOnline = false;
            userIdList.Add(item.UserId);
        }

        await _context.SaveChangesAsync();
        return userIdList;
    }
}
