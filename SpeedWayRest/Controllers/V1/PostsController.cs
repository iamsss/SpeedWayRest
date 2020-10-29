﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeedWayRest.Contracts;
using SpeedWayRest.Controllers.Requests;
using SpeedWayRest.Controllers.Responses;
using SpeedWayRest.Controllers.V1.Requests;
using SpeedWayRest.Domain;
using SpeedWayRest.Services;

namespace SpeedWayRest.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostServices _postServices;

        public PostsController(IPostServices postServices)
        {
            _postServices = postServices;
        }
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            var posts = _postServices.GetPosts();
            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute]string postId)
        {
            var post = _postServices.GetPostById(postId);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody]CreatePostRequest post)
        {
            var newPost = new Post(post.Id,post.Name);
            if (string.IsNullOrEmpty(newPost.Id))
            {
                newPost.Id = Guid.NewGuid().ToString();
            }
            _postServices.CreatePost(newPost);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id);

            var postResponse = new CreatePostResponse() { Id = post.Id , Name = post.Name};
            return Created(locationUri, postResponse);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute]string postId, [FromBody]UpdatePostRequest request)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return BadRequest("Post Id is Empty");
            }
            Post updatedPost = new Post(postId, request.Name);
            var isUpdated = _postServices.UpdatePost(updatedPost);
            if (isUpdated)
            {
                return Ok(updatedPost);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute]string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return BadRequest("Post Id is Empty");
            }

            var isDeleted = _postServices.DeletePost(postId);
            if (isDeleted)
                return NoContent();

            return NotFound();
        }
        }
}