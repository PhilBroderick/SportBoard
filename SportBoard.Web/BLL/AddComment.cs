using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class AddComment
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public AddComment(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        public bool TryAddComment(Post post, Comment comment)
        {
            return false;
        }
        
    }
}