using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface ICstatus 
    {
        Task<IEnumerable<Cstatus>> GetAll();
        
        Task<Cstatus> Create();
        Task<Cstatus> Read();
        Task<Cstatus> Update();
        Task<Cstatus> Delete();



    }
    public class CstatusRepository : ICstatus
    {
        private readonly MongoContext _context = null;

        public CstatusRepository(IOptions<Nochesettings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Cstatus>> GetAll()
        {
            try
            {
                return await _context.Cstatus.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<Cstatus> Create()
        {
            throw new NotImplementedException();
        }

        public Task<Cstatus> Delete()
        {
            throw new NotImplementedException();
        }

        

        public Task<Cstatus> Read()
        {
            throw new NotImplementedException();
        }

        public Task<Cstatus> Update()
        {
            throw new NotImplementedException();
        }
    }
}
