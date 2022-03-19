using Blog.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.User
{
    public class FollowAndFollowersDto
    {
        public List<UserFollowedsDto> UserFollowed { get; set; }
        public List<UserFollowersDto> UserFollower { get; set; }
    }
}
