using System;

namespace Domain
{
    public class UserActivity
    {
        public string AppUserId { get; set; }

        // to use lazy loading, add virtual key word to indicate this is a navigation property
        public virtual AppUser AppUser { get; set; }  // table中没有这一列，conversion
        public Guid ActivityId { get; set; }

        // to use lazy loading, add virtual key word to indicate this is a navigation property
        public virtual Activity Activity { get; set; }  // table中没有这一列，conversion
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}