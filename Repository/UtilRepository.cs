using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IUtil 
    {
        int SequenceValue(string collectionname);
    }
    public class UtilRepository : IUtil
    {
        private readonly MongoContext _context = null;

        public UtilRepository(IOptions<Mongosettings> settings)
        {
            _context = new MongoContext(settings);
        }
        public int SequenceValue(string collectionname)
        {

            var jsonQuery = "{ x : 3, y : 'abc' }";
            BsonDocument.Parse(jsonQuery);

            BsonDocument doc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(jsonQuery);
            var query = new QueryComplete(doc); // or probably Query.Wrap(doc);

            var jsonOrder = "{ x : 1 }";
            BsonDocument orderDoc = BsonSerializer.Deserialize<BsonDocument>(jsonQuery);

            var sortExpr = new SortByWrapper(orderDoc);



            throw new NotImplementedException();
            
        }
    }
}
