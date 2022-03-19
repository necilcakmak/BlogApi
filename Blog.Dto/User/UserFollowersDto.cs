using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.User
{
    public class UserFollowersDto
    {
        public Guid Id { get; set; }
        public Guid FollowersUserId { get; set; }
        public Guid UserId { get; set; }
        public UserDto UserDto { get; set; }
        public DateTime FollowedDate { get; set; }
    }
}
