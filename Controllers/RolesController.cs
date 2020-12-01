using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using noche.Context;
using noche.Models;
using noche.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoles _repository;

        public RolesController(IRoles ntrfc) { _repository = ntrfc; }


        [HttpPost]
        public async Task<ResponseNocheServices> Create(Roles values)
        {
            return await executeactionAsync(Action.CREATE, values: values);
        }

        [HttpGet]
        //public async Task<IEnumerable<Products>> Get()
        public async Task<ResponseNocheServices> Get()
        {
            return await executeactionAsync(Action.READALL);
        }

        [HttpGet("{id}")]
        public async Task<ResponseNocheServices> Read(string id)
        {
            return await executeactionAsync(Action.READID, id: id);
        }


        [HttpPut("{id}")]
        public async Task<ResponseNocheServices> Update(string id, Roles values)
        {
            return await executeactionAsync(Action.UPDATE, id: id, values: values);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseNocheServices> Delete(string id)
        {
            return await executeactionAsync(Action.DELETE, id: id);
        }

        [HttpDelete]
        [Route("{id}/physical")]
        public async Task<ResponseNocheServices> DeletePhysical(string id)
        {
            return await executeactionAsync(Action.DELETEPHYSICAL, id: id);
        }

        private async Task<ResponseNocheServices> executeactionAsync(Action action, string id = "", Roles values = null)
        {
            ResponseNocheServices rm = new ResponseNocheServices();
            var result = new Roles();

            try
            {
                switch (action)
                {
                    case Action.CREATE:
                        rm.Flag = await _repository.Create(values);
                        rm.Data = values;
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    case Action.READID:
                        rm.Data = await _repository.Read(id);
                        if (!string.IsNullOrEmpty(rm.Data.Id))
                            rm.SetResponse(1, string.Empty);
                        break;

                    case Action.READALL:
                        var list = await _repository.GetAll();
                        rm.Flag = list.Count() > 0 ? 1 : 0;
                        rm.Data = list;
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    case Action.UPDATE:
                        values.Id = id;
                        rm.Data = await _repository.Update(values);
                        if (!string.IsNullOrEmpty(rm.Data.Id))
                            rm.SetResponse(1, string.Empty);
                        break;

                    case Action.DELETE:
                        rm.Flag = await _repository.Delete(id);
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    case Action.DELETEPHYSICAL:
                        //rm.Flag = await _repository.DeletePhysical(id);
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                rm.Message = ex.Message;
            }
            return rm;
        }
        
    }
}