using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace noche.Context
{
    public class Roles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

    }

    public class Rol_Operation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string idrol { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string idoperation { get; set; }
    }
}
