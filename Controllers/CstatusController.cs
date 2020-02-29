using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noche.Repository;
using noche.Context;
namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CstatusController : ControllerBase
    {
        private readonly ICstatus _cstatus;
        public CstatusController(ICstatus cstatus)
        {
            _cstatus = cstatus;
        }

        [HttpGet]
        public async Task<IEnumerable<Cstatus>> GetAll()
        {
            return await _cstatus.GetAll();
        }
    }
}