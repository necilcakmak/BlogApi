using Blog.Entities.Entities.Base;

namespace Blog.Entities.Entities
{
    public class UserSetting : EntityBase
    {
        public bool ReceiveMail { get; set; }
        public bool NewBlog { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
