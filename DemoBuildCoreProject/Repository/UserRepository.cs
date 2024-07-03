using DemoBuildCoreProject.DBContext;
using DemoBuildCoreProject.Interface;
using HappyBookingShare.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoBuildCoreProject.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<UserModel>> GetAllData(string keyword, int pageIndex, int pageSize)
    {
        var result = await _context.UserRepository.Where(item => (item.FullName.Contains(keyword)
                                                                  || item.Address.Contains(keyword)
                                                                  || item.PhoneNumber.Contains(keyword)
                                                                  || item.CitizenIdentificationNumber.Contains(keyword))
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
            return new();
        }
        return new UserModel(userItem);
    }

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

    public async Task<UserModel> GetUserByLoginInfor(string userName, string password)
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
        return new UserModel(userItem);
    }

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
        }
        entity.FullName = userModel.FullName;
        entity.PhoneNumber = userModel.PhoneNumber;
        entity.Email = userModel.Email;
        entity.CitizenIdentificationNumber = userModel.CitizenIdentificationNumber;
        entity.Address = userModel.Address;
        entity.AvatarImage = userModel.AvatarImage;
        entity.Password = userModel.Password;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedId = userId;
        if (entity.UserId == 0)
        {
            _context.UserRepository.Add(entity);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task ReleaseResource()
    {
        await _context.DisposeAsync();
    }
}
