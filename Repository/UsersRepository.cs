using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using noche.Config;
using noche.Context;

namespace noche.Repository
{
    public interface IUsers
    {
        Task<bool> Delete(string id);
        Task<Users> Update(Users values);
        Task<Users> Read(string id);
        Task<bool> Create(Users values);
        Task<IEnumerable<Users>> GetAll();
    }
    public interface IRoles
    {
        Task<bool> Delete(string id);
        Task<Rols> Update(Rols values);
        Task<Rols> Read(string id);
        Task<bool> Create(Rols values);
        Task<IEnumerable<Rols>> GetAll();
    }

    public interface IRol_Operation
    {
        Task<bool> Delete(string id);
        Task<Rol_Operation> Update(Rol_Operation values);
        Task<Rol_Operation> Read(string id);
        Task<bool> Create(Rol_Operation values);
        Task<IEnumerable<Rol_Operation>> GetAll();
    }

    public interface IOperations
    {
        Task<bool> Delete(string id);
        Task<Operations> Update(Operations values);
        Task<Operations> Read(string id);
        Task<bool> Create(Operations values);
        Task<IEnumerable<Operations>> GetAll();
    }

    public interface IModules
    {
        Task<bool> Delete(string id);
        Task<Modules> Update(Modules values);
        Task<Modules> Read(string id);
        Task<bool> Create(Modules values);
        Task<IEnumerable<Modules>> GetAll();
    }

    public class UsersRepository : IUsers
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;
        // private readonly IProductRepository _productRepository = null;

        public UsersRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);

        }
        public async Task<bool> Create(Users values)
        {
            try
            {
                values. date_add = int.Parse(Util.ConvertToTimestamp());
                _context.Users.InsertOneAsync(values).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Users> Read(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> Update(Users values)
        {
            throw new NotImplementedException();
        }
    }

    public class RolesRepository : IRoles
    {
        public Task<bool> Create(Rols values)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rols>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Rols> Read(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Rols> Update(Rols values)
        {
            throw new NotImplementedException();
        }
    }
}
