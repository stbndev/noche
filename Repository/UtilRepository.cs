using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;

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





            throw new NotImplementedException();
            
        }

     

    }
}
