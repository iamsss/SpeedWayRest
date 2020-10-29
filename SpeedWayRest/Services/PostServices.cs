using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeedWayRest.Domain;

namespace SpeedWayRest.Services
{
    public class PostServices : IPostServices
    {
        private List<Post> _Posts = new List<Post>();
        public PostServices()
        {
            for (var i = 0; i < 5; i++)
            {

                _Posts.Add(new Post(Guid.NewGuid().ToString(), "Post " + i));

            }

        }
        public void CreatePost(Post post)
        {
            _Posts.Add(post);
        }
        public Post GetPostById(string postId)
        {
            return _Posts.Where(p => p.Id == postId).SingleOrDefault();
        }

        public List<Post> GetPosts()
        {
            return _Posts;
        }
    }
}
