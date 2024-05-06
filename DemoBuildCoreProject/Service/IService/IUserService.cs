using DemoBuildCoreProject.Model;

namespace DemoBuildCoreProject.Service.IService;

public interface IUserService
{
    Task<List<UserModel>> GetAllUserList();
}
