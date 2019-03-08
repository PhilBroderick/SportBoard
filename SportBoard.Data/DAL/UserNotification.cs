//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportBoard.Data.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserNotification
    {
        public int NotificationId { get; set; }
        public string UserIdToNotify { get; set; }
        public string UserIdCreatedNotification { get; set; }
        public NotificationTypes NotificationType { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public Nullable<int> FeedId { get; set; }
        public Nullable<int> PostId { get; set; }
        public Nullable<int> CommentId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
        public virtual UserNotification UserNotification1 { get; set; }
        public virtual UserNotification UserNotification2 { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual Feed Feed { get; set; }
        public virtual Post Post { get; set; }
    }
}
