using Blog.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Entities
{
    public class UserSetting : EntityBase
    {
        public bool ReceiveMail { get; set; }
        public bool NewBlog { get; set; }
        public bool IsApproved { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<FollowedAuthors> FollowedAuthors { get; set; }
        public ICollection<FollowersAuthors> FollowersAuthors { get; set; }
    }
}
