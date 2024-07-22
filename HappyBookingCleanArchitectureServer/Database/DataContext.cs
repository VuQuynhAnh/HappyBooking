using HappyBookingShare.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;
using System.Threading.Tasks;
using Sequence = HappyBookingShare.Entities.Sequence;

namespace HappyBookingCleanArchitectureServer.Database;

public class DataContext
{
    private readonly IMongoDatabase _database;

    public DataContext(IMongoClient client)
    {
        _database = client.GetDatabase("happy-booking"); // Tên cơ sở dữ liệu của bạn
    }

    public IMongoCollection<User> UserRepository => _database.GetCollection<User>("Users");
    public IMongoCollection<RefreshToken> RefreshTokenRepository => _database.GetCollection<RefreshToken>("RefreshTokens");
    public IMongoCollection<ImageManagement> ImageManagementRepository => _database.GetCollection<ImageManagement>("ImageManagements");
    public IMongoCollection<Setting> SettingRepository => _database.GetCollection<Setting>("Settings");
    public IMongoCollection<Sequence> SequenceRepository => _database.GetCollection<Sequence>("Sequences");

    public async Task<long> GetNextSequenceValue(string collectionName)
    {
        var filter = Builders<Sequence>.Filter.Eq(s => s.CollectionName, collectionName);
        var update = Builders<Sequence>.Update.Inc(s => s.Value, 1);
        var options = new FindOneAndUpdateOptions<Sequence>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        var sequence = await SequenceRepository.FindOneAndUpdateAsync(filter, update, options);
        return sequence.Value;
    }
}
