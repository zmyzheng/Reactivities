using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        // 向User table 多添加一个column
        public string DisplayName { get; set; }
    }
}