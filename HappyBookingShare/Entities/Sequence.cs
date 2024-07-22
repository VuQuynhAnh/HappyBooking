using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HappyBookingShare.Entities;

public class Sequence
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("CollectionName")]
    public string CollectionName { get; set; } = string.Empty;

    [BsonElement("Value")]
    public long Value { get; set; }
}
