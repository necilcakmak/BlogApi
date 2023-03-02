
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Dto.User
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public bool NewBlog { get; set; }
        public bool PasswordIsChange { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
        public string? PasswordRepeat { get; set; }
      
        public IFormFile? ImageFile { get; set; }
    }
}
