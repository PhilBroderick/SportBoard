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
            Images = new ImageRepository(_context);
            Posts = new PostRepository(_context);
            Users = new UserRepository(_context);
            UserPreferences = new UserPreferenceRepository(_context);
            DeletionRequests = new DeletionRequestRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
        }
        public IFeedRepository Feeds { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IImageRepository Images { get; private set; }
        public IPostRepository Posts { get; private set; }

        public IUserRepository Users { get; private set; }

        public IUserPreferenceRepository UserPreferences { get; private set; }

        public IDeletionRequestRepository DeletionRequests { get; private set; }

        public IUserNotificationRepository UserNotifications { get; private set; }

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
