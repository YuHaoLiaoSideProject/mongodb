using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongodb.Modelss.Base
{
    public class BaseMongoDBModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
