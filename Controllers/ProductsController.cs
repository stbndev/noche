using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noche.Context;
using noche.Models;
using noche.Repository;

namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly ProductsService _productService;
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        [HttpGet]
        //public async Task<IEnumerable<Products>> Get()
        public async Task<ResponseModel> Get()
        {
            return await executeactionAsync(Action.READALL);
        }

        //[HttpGet("{id}",Name = "ProductsGet")]
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<Products> Read(int id)
        {
            return await _repository.Read(id);
        }

        [HttpPost]
        public async Task<ResponseModel> Create(Products products)
        {
            return await executeactionAsync(Action.CREATE, value: products);
        }

        [HttpPut]
        public async Task<ResponseModel> Update(Products products)
        {
            return await executeactionAsync(Action.UPDATE, value: products);
        }
        private async Task<ResponseModel> executeactionAsync(Action action, int id = 0, Products value = null)
        {
            ResponseModel rm = new ResponseModel();
            Products result = new Products();

            try
            {

                switch (action)
                {
                    case Action.CREATE:
                        rm.response = await _repository.Create(value);
                        rm.result = value;
                        break;
                    case Action.READID:
                        //result = ng.Read(id: id).First();
                        break;
                    case Action.READALL:
                        var list = await _repository.GetAll();
                        rm.response = list.Count() > 0 ? true : false;
                        rm.result = list;
                        break;
                    case Action.UPDATE:
                         await _repository.Update(value);

                        //value.idproducts = id > 0 ? id : value.idproducts;
                        //result = ng.Update(value);
                        //rm.SetResponse(true);
                        break;
                    case Action.DELETE:
                        //bool flag = ng.Delete(id);
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