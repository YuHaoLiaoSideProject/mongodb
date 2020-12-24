using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace mongodb.Models.Base
{
    public class BaseMongoDBModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public ObjectId _id { get; set; }
        [BsonIgnore]
        public string Id 
        { 
            get 
            { 
                return _id.ToString(); 
            } 
        }
    }
}
