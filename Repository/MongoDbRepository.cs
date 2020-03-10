using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using noche.Config;
using System.Threading.Tasks;

namespace noche.Repository
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int idcompany { get; set; }

        [BsonRequired]
        [BsonDefaultValue(-1)]
        public int sequence_value { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int maker { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int date_add { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int date_set { get; set; }


    }
    public class MongoDbRepository<T> where T : EntityBase
    {
        private IMongoDatabase _database = null;
        private IMongoCollection<T> _collection;
        private readonly IOptions<Mongosettings> _mongosettings;


        public MongoDbRepository(IOptions<Mongosettings> settings)
        {
            _mongosettings = settings;
            var client = new MongoClient(_mongosettings.Value.ConnectionString);
            _database = client.GetDatabase(_mongosettings.Value.DatabaseName);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }


        public async Task<ReplaceOneResult> Save(T doc)
        {
            return await _collection.ReplaceOneAsync(w => w.Id == doc.Id, doc, new UpdateOptions { IsUpsert = true });
        }
    }
}
