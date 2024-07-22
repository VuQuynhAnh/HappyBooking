using MongoDB.Bson.Serialization.Attributes;

namespace HappyBookingShare.Entities;

public class Setting : BaseEntity
{
    [BsonId]
    [BsonElement("Id")]
    public long Id { get; set; }

    [BsonElement("UserId")]
    public long UserId { get; set; }

    [BsonElement("LanguageCode")]
    public string LanguageCode { get; set; } = string.Empty;
}
