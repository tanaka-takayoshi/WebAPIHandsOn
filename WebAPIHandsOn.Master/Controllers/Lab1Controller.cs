using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;
using Microsoft.Extensions.Configuration;

namespace WebAPIHandsOn.Controllers
{
    //TODO
    //Install-Package Rick.Docs.Samples.RouteInfo -Version 1.0.0.4
    //
    [Route("api/[controller]")]
    [ApiController]
    public class Lab1Controller : ControllerBase
    {
        private readonly IConfiguration configuration;

        //TODO ConfigurationのDI
        public Lab1Controller(IConfiguration configuration)
        {
            this.configuration = configuration;
            var env = configuration["ASPNETCORE_ENVIRONMENT"];
        }

        [HttpGet]   // GET /api/lab1
        public IActionResult ListProducts()
        {
            return ControllerContext.MyDisplayRouteInfo();
        }

        [HttpGet("{id}")]   // GET /api/lab1/xyz
        public IActionResult GetProduct(string id)
        {
            return ControllerContext.MyDisplayRouteInfo(id);
        }

        [HttpGet("int/{id:int}")] // GET /api/lab1/int/3
        public IActionResult GetIntProduct(int id)
        {
            return ControllerContext.MyDisplayRouteInfo(id);
        }

        [HttpGet("int2/{id}")]  // GET /api/lab1/int2/3
        public IActionResult GetInt2Product(int id)
        {
            return ControllerContext.MyDisplayRouteInfo(id);
        }
    }


}