using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(SportboardDbContext context) 
            : base(context)
        {
        }

        public SportboardDbContext SportboardDbContext
        {
            get { return Context as SportboardDbContext; }
        }

        public IQueryable<Comment> CommentsByHighestUpvote()
        {
            //var commentsByUpvotes = SportboardDbContext.Comment.Where()
            throw new NotImplementedException();
        }
    }
}
