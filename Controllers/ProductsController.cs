using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noche.Models;
using noche.Services;

namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productService;
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        [HttpGet]
        //public ActionResult<List<Products>> Get() =>
        //    _productService.Get();
        public async Task<IEnumerable<Products>> Get()
        {
            return await _repository.GetAll();
        }

    }
}