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
    
    public partial class DeletionRequest
    {
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public int FeedId { get; set; }
        public string ReasonForDeletion { get; set; }
        public bool RequestFulfilled { get; set; }
        public string AdminResponse { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Feed Feed { get; set; }
    }
}
