using System;
using System.Collections.Generic;
using Data.Entities;
using EFMC.Service.Common.Results;
using EFMC.Service.Models;

namespace EFMC.Service.Interfaces
{
    public interface IUserService
    {
        public Result<UserModel> Register(UserRegistration userRegistration);
        public Result<JwtLoginModel> Login(UserLogin userLogin);
        public Result<UserModel> GetUserInfo(int id);
        public Result<bool> LogOut(string userName);
    }
}
