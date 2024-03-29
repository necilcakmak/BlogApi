﻿using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Abstract
{
    public interface IUserSettingRepository : IEfRepositoryBase<UserSetting>
    {
        Task<UserSetting> GetMySettings();
    }
}
