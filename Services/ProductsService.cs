using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using noche.Models;
using noche.Config;

namespace noche.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Products> _products;
        private readonly IMongoDatabase _db = null;


        public ProductsService(DBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Products>("products");
        }

        public List<Products> Get() =>
            _products.Find(p => true).ToList();

    }

    public class ProductContext
    {
        private readonly IMongoDatabase _db = null;
        private readonly IOptions<Mongosettings> _mongosettings;

        IConfiguration configuration;


        public ProductContext(IOptions<Mongosettings> settings)
        {

            _mongosettings = settings;
            
            var client = new MongoClient(_mongosettings.Value.ConnectionString);
            if (client != null)
                _db = client.GetDatabase(_mongosettings.Value.DatabaseName);

        }

        public IMongoCollection<Products> Products
        {
            get
            {
                return _db.GetCollection<Products>("products");
            }
        }
    }

    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAll();
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context = null;

        public ProductRepository(IOptions<Mongosettings> settings)
        {
            _context = new ProductContext(settings);
        }
        public async Task<IEnumerable<Products>> GetAll()
        {
            try
            {
                return await _context.Products.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}
