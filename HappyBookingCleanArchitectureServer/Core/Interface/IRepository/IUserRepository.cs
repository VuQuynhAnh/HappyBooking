using HappyBookingShare.Entities;
using HappyBookingShare.Model;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IRepository;

public interface IUserRepository : IRepositoryBase
{
    Task<List<UserModel>> GetAllData(string keyword, int pageIndex, int pageSize);

    Task<UserModel> GetUserByUserId(long userId);

    Task<UserModel> GetUserByEmail(string email);

    Task<UserModel> GetUserByPhone(string phoneNumber);

    Task<UserModel> GetUserByCitizenIdentificationNumber(string citizenIdentificationNumber);

    Task<UserModel> GetUserByUserIdAndPassword(long userId, string password);

    Task<UserModel> GetUserByLoginInfor(string userName, string password);

    Task<bool> SaveUser(long userId, UserModel userModel);

    Task<string> GenerateRefreshToken(UserModel user);
}
