using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace mongodb.Modelss.Base
{
    public class BaseMongoDBModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        public string Id 
        { 
            get 
            { 
                return _id.ToString(); 
            } 
        }
    }
}
