
namespace Blog.Dto.User
{
    public class UserUpdateDto
    {
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public bool Gender { get; set; }
        public bool PasswordIsChange { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
        public string? PasswordRepeat { get; set; }
        public bool IsActive { get; set; }
    }
}
