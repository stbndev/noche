﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _repository;

        public UsersController(IUsers ius) { _repository = ius; }

        [HttpPost]
        [Route("signin")]

        public async Task<ResponseModel> Login(Users values)
        {
            ResponseModel rm = new ResponseModel();
            // Users result = new Users();
            try
            {
                rm.result = await _repository.Signin(values);
                rm.SetResponse(true);
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Revisar usuario/contraseña " + ex.Message);
            }

            return rm;
        }
        [HttpPost]
        public async Task<ResponseModel> Create(Users values)
        {
            return await executeactionAsync(Action.CREATE, values: values);
        }

        [HttpGet]
        //public async Task<IEnumerable<Products>> Get()
        public async Task<ResponseModel> Get()
        {
            return await executeactionAsync(Action.READALL);
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel> Read(string id)
        {
            return await executeactionAsync(Action.READID, id: id);
        }


        [HttpPut("{id}")]
        public async Task<ResponseModel> Update(string id, Users values)
        {
            return await executeactionAsync(Action.UPDATE, id: id, values: values);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(string id)
        {
            return await executeactionAsync(Action.DELETE, id: id);
        }

        [HttpDelete]
        [Route("{id}/physical")]
        public async Task<ResponseModel> DeletePhysical(string id)
        {
            return await executeactionAsync(Action.DELETEPHYSICAL, id: id);
        }

        private async Task<ResponseModel> executeactionAsync(Action action, string id = "", Users values = null)
        {
            ResponseModel rm = new ResponseModel();
            Users result = new Users();

            try
            {
                switch (action)
                {

                    case Action.CREATE:
                        rm.response = await _repository.Create(values);
                        rm.result = values;
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.READID:
                        rm.result = await _repository.Read(id);
                        if (!string.IsNullOrEmpty(rm.result.Id))
                            rm.SetResponse(true, string.Empty);
                        break;

                    case Action.READALL:
                        var list = await _repository.GetAll();
                        rm.response = list.Count() > 0 ? true : false;
                        rm.result = list;
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.UPDATE:
                        values.Id = id;
                        rm.result = await _repository.Update(values);
                        if (!string.IsNullOrEmpty(rm.result.Id))
                            rm.SetResponse(true, string.Empty);
                        break;

                    case Action.DELETE:
                        rm.response = await _repository.Delete(id);
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.DELETEPHYSICAL:
                        //rm.response = await _repository.DeletePhysical(id);
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }
            return rm;
        }
    }
}