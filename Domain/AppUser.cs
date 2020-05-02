using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        // 向User table 多添加一个column
        public string DisplayName { get; set; }
        public string Bio { get; set; }

    // to use lazy loading, add virtual key word to indicate this is a navigation property
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}