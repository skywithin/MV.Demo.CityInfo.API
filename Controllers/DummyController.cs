using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MV.Demo.CityInfo.API.Contexts;

namespace MV.Demo.CityInfo.API.Controllers
{
    [Route("api/testdatabase")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
