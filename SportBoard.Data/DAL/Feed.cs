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
    
    public partial class Feed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feed()
        {
            this.Post = new HashSet<Post>();
        }
    
        public int FeedId { get; set; }
        public string FeedName { get; set; }
        public string UserId { get; set; }
        public int ImageId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Image Image { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }
    }
}
