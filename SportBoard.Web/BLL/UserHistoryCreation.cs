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
                _feeds = context.Feed
                                .Include("Image")
                                .Where(f => f.UserId == id)
                                .OrderByDescending(f => f.CreatedOn)
                                .ToList();

                _posts = context.Post
                                .Include("Feed")
                                .Include("Image")
                                .Where(p => p.UserId == id)
                                .OrderByDescending(p => p.PostDate)
                                .ToList();

                _comments = context.Comment
                                    .Include("Post")
                                    .Include("CommentUpvoteUserIds")
                                    .Include("CommentDownVoteUserIds")
                                    .Where(c => c.UserId == id)
                                    .OrderByDescending(c => c.CreatedOn)
                                    .ToList();

                _deletionRequests = context.DeletionRequest
                                            .Include("Feed")
                                            .Where(dr => dr.UserId == id)
                                            .ToList();
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
            };
        }

        public UserHistory CreatePostModel()
        {
            return new UserHistory
            {
                Posts = _posts
            };
        }

        public UserHistory CreateCommentModel()
        {
            return new UserHistory
            {
                Comments = _comments
            };
        }

        public UserHistory CreateDeletionModel()
        {
            return new UserHistory
            {
                DeletionRequests = _deletionRequests
            };
        }
      
    }
}