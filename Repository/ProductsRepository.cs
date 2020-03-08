using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IProductRepository
    {
        Task<bool> Update(Products values);
        Task<Products> Read(int sequence_value, string id = "");
        Task<bool> Create(Products products);
        Task<IEnumerable<Products>> GetAll();
        //Task<bool> Create(Products products);

    }
    public class ProductsRepository : IProductRepository
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Mongosettings> _mongosettings;


        public ProductsRepository(IOptions<Mongosettings> settings)
        {
            _mongosettings = settings;
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
        public async Task<bool> Create(Products products)
        {
            try
            {
                int sequence_value = _context.ProductsNext();
                products.sequence_value = ++sequence_value;
                _context.Products.InsertOneAsync(products).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Products> Read(int sequence_value, string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    //return await _context.Products.Find(x => x.sequence_value == sequence_value).FirstAsync<Products>();
                }
                else
                {
                    //return await _context.Products.Find(x => x.Id == id).FirstAsync<Products>();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Task<bool> Update(Products values)
        //{
        //    var userRepo = new MongoDbRepository<Products>(_mongosettings);
        //    //userRepo.Save(new Products
        //    //{
        //    //    FirstName = "fn",
        //    //    LastName = "ln"
        //    //}).Wait();
        //    userRepo.Save(values).Wait();

        //    throw new NotImplementedException();
        //}

        public async Task<bool> Update(Products values)
        {
            var userRepo = new MongoDbRepository<Products>(_mongosettings);
            //userRepo.Save(new Products
            //{
            //    FirstName = "fn",
            //    LastName = "ln"
            //}).Wait();
            userRepo.Save(values).Wait();

            throw new NotImplementedException();
        }
    }
}
