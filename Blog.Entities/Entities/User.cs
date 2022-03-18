using Blog.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public UserSetting UserSetting { get; set; }
        public ICollection<UserFollower> UserFollowers { get; set; }
        public ICollection<UserFollowed> UserFollowed { get; set; }
        public ICollection<Article>? Articles { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }
}
