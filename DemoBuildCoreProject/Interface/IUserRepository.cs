using DemoBuildCoreProject.Model;

namespace DemoBuildCoreProject.Interface;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllData(string keyword);
}
