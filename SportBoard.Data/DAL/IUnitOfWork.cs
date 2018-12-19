using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IFeedRepository Feeds { get; }
        IImageRepository Images { get; }
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        IUserRepository Users { get; }
        IUserPreferenceRepository UserPreferences { get; }
        int Complete();
    }
}
