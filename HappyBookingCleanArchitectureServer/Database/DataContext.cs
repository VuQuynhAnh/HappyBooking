using HappyBookingShare.Entities;
using Microsoft.EntityFrameworkCore;

namespace HappyBookingCleanArchitectureServer.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> UserRepository { get; set; }

    public DbSet<RefreshToken> RefreshTokenRepository { get; set; }

    public DbSet<ImageManagement> ImageManagementRepository { get; set; }

    public DbSet<Setting> SettingRepository { get; set; }

    public DbSet<Chat> ChatRepository { get; set; }

    public DbSet<Message> MessageRepository { get; set; }

    public DbSet<ChatParticipant> ChatParticipantRepository { get; set; }
}
