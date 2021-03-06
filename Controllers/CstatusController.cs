﻿using Microsoft.AspNetCore.Mvc;
using noche.Context;
using noche.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
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