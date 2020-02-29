using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAll();
    }
    public class ProductsRepository : IProductRepository
    {
        private readonly MongoContext _context = null;
        public ProductsRepository(IOptions<Mongosettings> settings) 
        {
            _context = new MongoContext(settings);
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
