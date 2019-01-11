using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class VotingEnums
    {

        public enum PostVotingEnum
        {
            ThumbsDown = 0,
            ThumbsUp = 1
        }

        public enum CommentVotingEnum
        {
            Downvote = 0,
            Upvote = 1
        }
    }
}