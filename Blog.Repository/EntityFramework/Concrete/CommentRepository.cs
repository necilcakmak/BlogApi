using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class CommentRepository : EfRepositoryBase<Comment>, ICommentRepository
    {
        BlogDbContext _dbContext;
        public CommentRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
            _dbContext = context;
        }

        public async Task<bool> DeleteMyComment(Guid id)
        {
            var comment = await _dbContext.Comments.Where(x => x.Id == id && x.UserId == UserId).FirstOrDefaultAsync();
            if (comment != null)
            {
                await Task.Run(() => { _dbContext.Comments.Remove(comment); });
                return true;
            }
            return false;

        }
    }
}
