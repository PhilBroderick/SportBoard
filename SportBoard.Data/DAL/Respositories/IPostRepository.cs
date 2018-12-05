using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> GetTopFivePostsByComment();

        IQueryable<Post> GetTopFivePostsByLikes();

        IQueryable<Post> SortPostsByRating(string sortOrder);
    }
}
