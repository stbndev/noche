using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IRoles
    {
        Task<bool> Delete(string id);
        Task<Roles> Update(Roles values);
        Task<Roles> Read(string id);
        Task<bool> Create(Roles values);
        Task<IEnumerable<Roles>> GetAll();


    }
    public class RolesRepository : IRoles
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;

        public RolesRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
        }

        public async Task<bool> Create(Roles values)
        {
            try
            {
                _context.Roles.InsertOneAsync(values).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                FilterDefinition<Roles> filter = Builders<Roles>.Filter.Eq(s => s.Id, id);
                var delete_result = await _context.Roles.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Roles>> GetAll()
        {
            try
            {
                return await _context.Roles.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Roles> Read(string id)
        {
            try
            {
                return await _context.Roles.Find(x => x.Id == id).FirstAsync<Roles>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Roles> Update(Roles values)
        {
            try
            {
                var update = Builders<Roles>.Update
                .Set(x => x.name, values.name);

                FilterDefinition<Roles> filter = Builders<Roles>.Filter.Eq(s => s.Id, values.Id);
                await _context.Roles.UpdateOneAsync(filter, update);
                var result = await Read(values.Id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
    }
}
