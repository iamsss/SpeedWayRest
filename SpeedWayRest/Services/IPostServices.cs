using SpeedWayRest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWayRest.Services
{
    public interface IPostServices
    {
        List<Post> GetPosts();
        Post GetPostById(string postId);
        void CreatePost(Post post);
        bool UpdatePost(Post postToUpdate);
        bool DeletePost(string postId);
    }
}
