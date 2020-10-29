using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeedWayRest.Contracts;
using SpeedWayRest.Domain;

namespace SpeedWayRest.Controllers
{
    public class PostsController : Controller
    {
        private List<Post> _Posts = new List<Post>();
        public PostsController()
        {
            _Posts.Add(new Post("First Post"));
            _Posts.Add(new Post("Second Post"));
            _Posts.Add(new Post("Thrid Post"));
            _Posts.Add(new Post("Fourth Post"));

        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult Index()
        {
            return Ok(_Posts);
        }
    }
}