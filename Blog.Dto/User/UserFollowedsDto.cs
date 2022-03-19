using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.User
{
    public class UserFollowedsDto
    {
        public Guid Id { get; set; }
        public Guid FollowedUserId { get; set; }
        public Guid UserId { get; set; }
        public UserDto UserDto { get; set; }
        public DateTime FollowedDate { get; set; }
    }
}
