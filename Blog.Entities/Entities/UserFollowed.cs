﻿using Blog.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Entities
{
    public class UserFollowed : IEntity
    {
        public Guid Id { get; set; }
        public Guid FollowedUserId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime FollowedDate { get; set; }
    }
}