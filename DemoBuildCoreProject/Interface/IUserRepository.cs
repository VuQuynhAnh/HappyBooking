using DemoBuildCoreProject.Model;

namespace DemoBuildCoreProject.Interface;

public interface IUserRepository : IRepositoryBase
{
    Task<List<UserModel>> GetAllData(string keyword, int pageIndex, int pageSize);

    Task<UserModel> GetUserByUserId(long userId);

    Task<UserModel> GetUserByLoginInfor(string userName, string password);
}
