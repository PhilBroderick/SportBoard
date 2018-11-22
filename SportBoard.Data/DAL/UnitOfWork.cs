using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportBoard.Data.DAL.Respositories;

namespace SportBoard.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportboardDbContext _context;

        public UnitOfWork(SportboardDbContext context)
        {
            _context = context;
            Feeds = new FeedRepository(_context);
            Comments = new CommentRepository(_context);
        }
        public IFeedRepository Feeds { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
