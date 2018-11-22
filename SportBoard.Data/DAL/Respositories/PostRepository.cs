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
    }
}
