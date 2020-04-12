using System;

namespace Domain
{
    public class UserActivity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }  // table中没有这一列，conversion
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }  // table中没有这一列，conversion
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}