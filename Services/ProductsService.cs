using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Models;

namespace noche.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Products> _products;

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

        public ProductContext(IOptions<Settings> settings)
        {
            //var client = new MongoClient(settings.Value.ConnectionString);
            //if (client != null)
            //    _db = client.GetDatabase(settings.Value.Database);
            var client = new MongoClient("mongodb+srv://dbuser:develop3r@cluster0-pd5jd.gcp.mongodb.net/test?retryWrites=true&w=majority");
            if (client != null)
                _db = client.GetDatabase("mrgvndb");

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

        public ProductRepository(IOptions<Settings> settings)
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
