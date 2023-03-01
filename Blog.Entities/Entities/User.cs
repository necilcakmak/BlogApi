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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public bool IsApproved { get; set; }
        public string RoleName { get; set; }
        public string ImageName { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual UserSetting UserSetting { get; set; }
        public virtual ICollection<Article>? Articles { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
