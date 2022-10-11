using Microsoft.AspNetCore.Identity;

namespace JAPManagementSystem.Models.UserModel
{
    public abstract class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
