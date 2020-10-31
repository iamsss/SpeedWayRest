using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeedWayRest.Contracts;
using SpeedWayRest.Controllers.Requests;
using SpeedWayRest.Controllers.Responses;
using SpeedWayRest.Controllers.V1.Requests;
using SpeedWayRest.Domain;
using SpeedWayRest.Services;

namespace SpeedWayRest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostServices _postServices;

        public PostsController(IPostServices postServices)
        {
            _postServices = postServices;
        }
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postServices.GetPostsAsync();
            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]int postId)
        {
            var post = await _postServices.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody]CreatePostRequest post)
        {
            var newPost = new Post(post.Name);
           
            var createdPost = await _postServices.CreatePostAsync(newPost);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id.ToString());

            var postResponse = new CreatePostResponse() { Id = createdPost.Id , Name = post.Name};
            return Created(locationUri, postResponse);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute]int postId, [FromBody]UpdatePostRequest request)
        {
            if(!(postId > 0))
            {
                return BadRequest("Post Not Found");
            }
            Post updatedPost = new Post(request.Name);
            updatedPost.Id = postId;
            var isUpdated = await _postServices.UpdatePostAsync(updatedPost);
            if (isUpdated)
            {
                return Ok(updatedPost);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute]int postId)
        {

            if (!(postId > 0))
            {
                return BadRequest("Post Id is Empty");
            }

            var isDeleted = await _postServices.DeletePost(postId);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }
        }
}