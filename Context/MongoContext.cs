﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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

        IConfiguration configuration;


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
    }
}