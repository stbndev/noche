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
        Task<bool> Create(Products products);
        Task<IEnumerable<Products>> GetAll();
        Task<Products> Create(Products products);
        Task<Products> Read(int sequence_value);


    }
    public class ProductsRepository : IProductRepository
    {
        private readonly MongoContext _context = null;
        public ProductsRepository(IOptions<Mongosettings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<bool> Create(Products products)
        {
            try
            {
                await _context.Products.InsertOneAsync(products);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<Products> Create(Products products)
        {
            try
            {
                _context.Products.InsertOneAsync(products).Wait();
                return products;
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
        public async Task<Products> Update(Products products)
        {
            try
            {
                return _context.Products.FindOneAndReplace(products);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
