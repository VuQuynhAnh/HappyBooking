using DemoBuildCoreProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoBuildCoreProject.DBContext;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> UserRepository { get; set; }
}
