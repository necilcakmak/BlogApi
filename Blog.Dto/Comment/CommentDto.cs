﻿using Blog.Dto.Article;
using Blog.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Comment
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
        public string? Text { get; set; }
        public UserDto? User { get; set; }
    }
}
