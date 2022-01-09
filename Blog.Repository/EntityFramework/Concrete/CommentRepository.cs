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
        public CommentRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
        }

        public async Task<bool> DeleteMyComment(Guid id)
        {
            IQueryable<Comment> query = _context.Set<Comment>();
            query = query.Where(x => x.Id == id && x.UserId == UserId);
            var comment = await query.FirstOrDefaultAsync();
            if (comment != null)
            {
                await Task.Run(() => { _context.Set<Comment>().Remove(comment); });
                return true;
            }
            return false;

        }
    }
}
