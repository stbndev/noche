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
    public class OperationsController : ControllerBase
    {
        private readonly IOperations _repository;

        public OperationsController(IOperations ius) { _repository = ius; }

        [HttpPost]
        public async Task<ResponseNocheServices> Create(Operations values)
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
        public async Task<ResponseNocheServices> Update(string id, Operations values)
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

        private async Task<ResponseNocheServices> executeactionAsync(Action action, string id = "", Operations values = null)
        {
            ResponseNocheServices rm = new ResponseNocheServices();
            Operations result = new Operations();

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
                        rm.Flag = list.Count() > 0 ? 1: 0;
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