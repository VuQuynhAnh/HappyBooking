using DemoBuildCoreProject.DBContext;
using DemoBuildCoreProject.Interface;
using DemoBuildCoreProject.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoBuildCoreProject.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<UserModel>> GetAllData(string keyword)
    {
        var result = await _context.UserRepository.Select(item => new UserModel(item)).ToListAsync();
        return result;
    }
}
