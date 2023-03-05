using Blog.Core.Results;
using Blog.Dto.User;
using Microsoft.AspNetCore.Http;
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
        Task<Result> GetList();
        Task<Result> Delete(Guid id);
        Task<Result> DeleteList(List<Guid> idList);
        Task<Result> UpdateMyInformation(UserUpdateDto userUpdateDto, string webRootPage);
        Task<Result> UpdateMySettings(UserSettingDto userSettingDto);
        Task<Result> SendNewPostMail();
    }
}
