using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace noche.Context
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string email { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string password { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string idrol { get; set; }

        [BsonDefaultValue(0)]
        public int date_add { get; set; }

        public Companies company { get; set; }

    }


    

    
}
