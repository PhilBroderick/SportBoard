using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class Comments
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public Comments(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        public void CreateNewComment(Comment comment)
        {
            _unitOfWork.Comments.Add(comment);
            _unitOfWork.Complete();
        }

        public void AddComentUpvote(Comment comment)
        {
            _unitOfWork.Comments.Update(comment);
            _unitOfWork.Complete();
        }
        
    }
}