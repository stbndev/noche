using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
        private readonly IOptions<Nochesettings> _mongosettings;

        public MongoContext(IOptions<Nochesettings> settings)
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
        public IMongoCollection<Entries> Entries
        {
            get
            {
                return _db.GetCollection<Entries>("entries");
            }
        }
        public int EntriesNext()
        {
            int result = 0;
            try
            {
                var collection = _db.GetCollection<Entries>("entries");
                result = (from c in collection.AsQueryable<Entries>() select c.identries).Max();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    return result;
                else
                    throw ex;
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
            int result = 0;
            try
            {
                var collection = _db.GetCollection<Products>("products");
                result = (from c in collection.AsQueryable<Products>() select c.idproducts).Max();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    return result;
                else
                    throw ex;
            }
        }

        public IMongoCollection<Sales> Sales
        {
            get
            {
                return _db.GetCollection<Sales>("sales");
            }
        }
        public int SalesNext()
        {
            int result = 0;
            try
            {
                var collection = _db.GetCollection<Sales>("sales");
                result = (from c in collection.AsQueryable<Sales>() select c.idsales).Max();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    return result;
                else
                    throw ex;
            }
        }

        public IMongoCollection<Shrinkages> Shrinkages
        {
            get
            {
                return _db.GetCollection<Shrinkages>("shrinkages");
            }
        }

        public int ShrinkageNext()
        {
            int result = 0;
            try
            {
                var collection = _db.GetCollection<Shrinkages>("shrinkages");
                result = (from c in collection.AsQueryable<Shrinkages>() select c.idshrinkages).Max();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    return result;
                else
                    throw ex;
            }
        }
    }
}
