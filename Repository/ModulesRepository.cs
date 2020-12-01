using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IModules
    {
        Task<int> Delete(string id);
        Task<Modules> Update(Modules values);
        Task<Modules> Read(string id);
        Task<int> Create(Modules values);
        Task<IEnumerable<Modules>> GetAll();
    }
    public class ModulesRepository : IModules
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;

        public ModulesRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);

        }

        public async Task<int> Create(Modules values)
        {
            try
            {
                _context.Modules.InsertOneAsync(values).Wait();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(string id)
        {
            try
            {
                FilterDefinition<Modules> filter = Builders<Modules>.Filter.Eq(s => s.Id, id);
                var delete_result = await _context.Modules.DeleteOneAsync(filter);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Modules>> GetAll()
        {
            try
            {
                return await _context.Modules.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Modules> Read(string id)
        {
            try
            {
                return await _context.Modules.Find(x => x.Id == id).FirstAsync<Modules>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Modules> Update(Modules values)
        {
            try
            {
                var update = Builders<Modules>.Update
                .Set(x => x.name, values.name);

                FilterDefinition<Modules> filter = Builders<Modules>.Filter.Eq(s => s.Id, values.Id);
                await _context.Modules.UpdateOneAsync(filter, update);
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
