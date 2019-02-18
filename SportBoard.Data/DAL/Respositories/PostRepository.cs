using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(SportboardDbContext context) 
            : base(context)
        {
        }

        public IQueryable<Post> GetTopFivePosts()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> GetTopFivePostsByComment()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> GetTopFivePostsByLikes()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> SortPostsByRating(string sortOrder)
        {
            IQueryable<Post> posts;
            if(sortOrder == "best")
            {
                posts = SportboardDbContext.Post.OrderByDescending(p => p.PostThumbsUpUserIds.Count).ThenByDescending(p => p.Comment.Count);
            }
            posts = SportboardDbContext.Post.OrderByDescending(p => p.PostThumbsUpUserIds.Count).ThenBy(p => p.PostThumbsDownUserIds.Count);

            return posts;
        }

        public bool SearchIfPostExistsById(int id)
        {
            return SportboardDbContext.Post.Any(p => p.PostId == id);
        }
        public SportboardDbContext SportboardDbContext
        {
            get { return Context as SportboardDbContext; }
        }
    }
}
