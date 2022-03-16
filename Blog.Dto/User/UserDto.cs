using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.User
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public UserSettingDto UserSetting { get; set; }
    }
}
