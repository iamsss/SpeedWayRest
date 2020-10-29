using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeedWayRest.Data;
using SpeedWayRest.Domain;

namespace SpeedWayRest.Services
{
    public class PostServices : IPostServices
    {
        private readonly DataContext _context;

        public PostServices(DataContext db)
        {
            _context = db;
        }
        
        public async Task<Post> CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            var createdPost = await _context.SaveChangesAsync();
            return post;
            
        }

        public async Task<bool> DeletePost(int postId)
        {
            var post = await GetPostByIdAsync(postId);
            if(post == null)
            {
                return false;
            }

            _context.Posts.Remove(post);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async  Task<Post> GetPostByIdAsync(int postId)
        {
            var post = await _context.Posts.Where(p => p.Id == postId).SingleOrDefaultAsync();
            return post;
        }

        public async  Task<List<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _context.Posts.Update(postToUpdate);
            var updatedPost = await _context.SaveChangesAsync();
            return updatedPost > 0;
        }
    }
}
