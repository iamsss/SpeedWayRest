using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeedWayRest.Contracts;
using SpeedWayRest.Controllers.Requests;
using SpeedWayRest.Controllers.Responses;
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

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Index([FromBody]CreatePostRequest post)
        {
            var newPost = new Post(post.Id);
            if (string.IsNullOrEmpty(newPost.Id))
            {
                newPost.Id = Guid.NewGuid().ToString();
            }
            _Posts.Add(newPost);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id);

            var postResponse = new CreatePostResponse() { Id = post.Id };
            return Created(locationUri, postResponse);
        }
    }
}