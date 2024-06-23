using HappyBookingServer.DBContext;
using HappyBookingServer.Interface;
using HappyBookingShare.Model;
using Microsoft.EntityFrameworkCore;

namespace HappyBookingServer.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<UserModel>> GetAllData(string keyword, int pageIndex, int pageSize)
    {
        var result = await _context.UserRepository.Where(item => (item.UserName.Contains(keyword)
                                                                  || item.Description.Contains(keyword))
                                                                 && item.IsDeleted == 0)
                                                  .Skip((pageIndex - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .Select(item => new UserModel(item))
                                                  .ToListAsync();
        return result;
    }

    public async Task<UserModel> GetUserByUserId(long userId)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserId == userId
                                                                                 && item.IsDeleted == 0);
        if (userItem == null)
        {
            return new UserModel();
        }
        return new UserModel(userItem);
    }

    public async Task<UserModel> GetUserByLoginInfor(string userName, string password)
    {
        var userItem = await _context.UserRepository.FirstOrDefaultAsync(item => item.UserName == userName
                                                                                 && item.Password == password
                                                                                 && item.IsDeleted == 0);
        if (userItem == null)
        {
            return new UserModel();
        }
        return new UserModel(userItem);
    }

    public async Task ReleaseResource()
    {
        await _context.DisposeAsync();
    }
}
