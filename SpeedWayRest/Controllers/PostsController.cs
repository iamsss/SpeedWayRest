using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SpeedWayRest.Controllers
{
    public class PostsController : Controller
    {
        [HttpGet("/api/test")]
        public IActionResult Index()
        {
            return Ok("I am Fine Working");
        }
    }
}