using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.User
{
    public class UserSettingDto
    {
        public bool NewBlog { get; set; }
        public bool ReceiveMail { get; set; }
        public bool IsApproved { get; set; }
    }
}
