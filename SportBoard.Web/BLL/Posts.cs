using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class Posts
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Posts(IFeedRepository feedRepository, IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _feedRepository = feedRepository;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public bool TryCreatePost(Post post)
        {
            if(post != null)
            {
                CreateNewPost(post);
                return true;
            }
            return false;
        }

        private void CreateNewPost(Post post)
        {
            _unitOfWork.Posts.Add(post);
            _unitOfWork.Complete();
        }

        public void UpdatePost(Post post)
        {
            _unitOfWork.Posts.Update(post);
            _unitOfWork.Complete();
        }

        public void DeletePost(Post post)
        {
            _unitOfWork.Posts.Remove(post);
            _unitOfWork.Complete();
        }
    }
}