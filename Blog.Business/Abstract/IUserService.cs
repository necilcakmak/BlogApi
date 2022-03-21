using Blog.Core.Results;
using Blog.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract
{
    public interface IUserService
    {
        Task<Result> UserInformation();
        Task<Result> Get(Guid id);
        Task<Result> Delete(Guid id);
        Task<Result> UpdateMyInformation(UserUpdateDto userUpdateDto);
        Task<Result> UpdateMySettings(UserSettingDto userSettingDto);
        Task<Result> SendNewPostMail();
    }
}
