using HappyBookingShare.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoBuildCoreProject.DBContext;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> UserRepository { get; set; }

    public DbSet<RefreshToken> RefreshTokenRepository { get; set; }

    public DbSet<ImageManagement> ImageManagementRepository { get; set; }
}
