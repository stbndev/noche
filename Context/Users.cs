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

    }

    public class Rols
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

    }

    public class Rol_Operation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string idrol { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string idoperation { get; set; }
    }

    public class Operations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string idmodules { get; set; }
    }

    public class Modules
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }
    }
}
