using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using noche.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class MongoContext
    {
        private readonly IMongoDatabase _db = null;
        private readonly IOptions<Mongosettings> _mongosettings;

        //IConfiguration configuration;


        public MongoContext(IOptions<Mongosettings> settings)
        {

            _mongosettings = settings;

            var client = new MongoClient(_mongosettings.Value.ConnectionString);
            if (client != null)
                _db = client.GetDatabase(_mongosettings.Value.DatabaseName);

        }

        public IMongoCollection<Cstatus> Cstatus
        {
            get
            {
                return _db.GetCollection<Cstatus>("cstatus");
            }
        }
        public IMongoCollection<Products> Products
        {
            get
            {
                return _db.GetCollection<Products>("products");
            }
        }

        public int ProductsNext()
        {
            var collection = _db.GetCollection<Products>("products");
            var result = (from c in collection.AsQueryable<Products>() select c.sequence_value).Max();
            return result;
        }
        public IMongoCollection<ProductEntries> ProductEntries
        {
            get { return _db.GetCollection<ProductEntries>("productsentries"); }
        }
        public IMongoCollection<ProductEntryDetails> ProductEntryDetails
        {
            get
            {
                return _db.GetCollection<ProductEntryDetails>("productsentrydetails");
            }
        }

        public IMongoCollection<Sales> Sales
        {
            get
            {
                return _db.GetCollection<Sales>("sales");
            }
        }
        public IMongoCollection<SalesDetails> SalesDetails
        {
            get
            {
                return _db.GetCollection<SalesDetails>("salesdetails");
            }
        }
        public IMongoCollection<Shrinkage> Shrinkage
        {
            get
            {
                return _db.GetCollection<Shrinkage>("shrinkage");
            }
        }

        public IMongoCollection<ShrinkageDetails> ShrinkageDetails
        {
            get
            {
                return _db.GetCollection<ShrinkageDetails>("shrinkagedetails");
            }
        }



    }
}
