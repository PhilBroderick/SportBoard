using SportBoard.Data.DAL;
using SportBoard.Web.Models.HistoricalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class UserHistoryCreation
    {
        private List<Feed> _feeds;
        private List<Post> _posts;
        private List<Comment> _comments;
        private List<DeletionRequest> _deletionRequests;

        public UserHistoryCreation(string userId)
        {
            GetUserHistory(userId);
        }

        public void GetUserHistory(string id)
        {
            using (var context = new SportboardDbContext())
            {
                _feeds = context.Feed.Where(f => f.UserId == id).ToList();
                _posts = context.Post.Where(p => p.UserId == id).ToList();
                _comments = context.Comment.Where(c => c.UserId == id).ToList();
                _deletionRequests = context.DeletionRequest.Where(dr => dr.UserId == id).ToList();
            }
        }

        public UserHistory CreateModel()
        {
            return new UserHistory
            {
                Feeds = _feeds,
                Posts = _posts,
                Comments = _comments,
                DeletionRequests = _deletionRequests
            };
        }  
        
        public UserHistory CreateFeedModel()
        {
            return new UserHistory
            {
                Feeds = _feeds
                //Posts = new List<Post>(),
                //Comments = new List<Comment>(),
                //DeletionRequests = new List<DeletionRequest>()
            };
        }

        public UserHistory CreatePostModel()
        {
            return new UserHistory
            {
                Posts = _posts
                //Feeds = new List<Feed>(),
                //Comments = new List<Comment>(),
                //DeletionRequests = new List<DeletionRequest>()
            };
        }

        public UserHistory CreateCommentModel()
        {
            return new UserHistory
            {
                Posts = new List<Post>(),
                Feeds = new List<Feed>(),
                Comments = _comments,
                DeletionRequests = new List<DeletionRequest>()
            };
        }

        public UserHistory CreateDeletionModel()
        {
            return new UserHistory
            {
                Posts = new List<Post>(),
                Feeds = new List<Feed>(),
                Comments = new List<Comment>(),
                DeletionRequests = _deletionRequests
            };
        }
      
    }
}