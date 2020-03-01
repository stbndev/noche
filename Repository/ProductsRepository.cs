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
        Task<Products> Read(int sequence_value);
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

        public async Task<Products> Read(int sequence_value)
        {
            try
            {
                return await _context.Products.Find(x => x.sequence_value == sequence_value).FirstAsync<Products>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
