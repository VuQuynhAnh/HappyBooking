using DemoBuildCoreProject.Interface;
using DemoBuildCoreProject.Model;
using DemoBuildCoreProject.Service.IService;

namespace DemoBuildCoreProject.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserModel>> GetAllUserList()
    {
        var result = await _userRepository.GetAllData(string.Empty);
        return result;
    }
}
