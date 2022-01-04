using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using EFMC.Data.Interfaces;
using EFMC.Service.Common;
using EFMC.Service.Common.Constants;
using EFMC.Service.Common.Enums;
using EFMC.Service.Common.Results;
using EFMC.Service.Common.Utils;
using EFMC.Service.Interfaces;
using EFMC.Service.Models;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace EFMC.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        private MessageResult messageResult;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            messageResult = memoryCache.GetOrCreate(
                CacheMessageConstant.USER,
                userMessage =>
                {
                    return CacheEngine.GetMessages(MessageDomainConstant.USER);
                }
                );
        }

        public Result<UserModel> GetUserInfo(int id)
        {
            try
            {
                // Get user by user id
                var user = userRepository.Get(u => u.UserId == id);

                // Check user existed?
                if (user == null)
                    return new Result<UserModel>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(
                            MessageUserConstant.NOT_FOUND_USER,
                            messageResult.Messages
                            )
                    };
                return new Result<UserModel>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = user.Adapt<UserModel>(),
                    Message = MessageUtils.Message(
                        MessageUserConstant.GET_INFORMATION_SUCCESS,
                        messageResult.Messages
                        )
                };
            }
            catch (Exception e)
            {
                return new Result<UserModel>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        public Result<JwtLoginModel> Login(UserLogin userLogin)
        {

            try
            {

                // Get user by UserName
                var user = userRepository.Get(u => u.UserName == userLogin.UserName);
                if (user == null)
                {
                    return new Result<JwtLoginModel>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(
                            MessageUserConstant.NOT_FOUND_USER,
                            messageResult.Messages
                            )
                    };
                }

                // Check status of user. Status must ACTIVATE
                if (user.Status != UserStatusEnumeration.Activate.ToString())
                {
                    return new Result<JwtLoginModel>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(
                            MessageUserConstant.NOT_ACTIVATE_USER,
                            messageResult.Messages
                            )
                    };
                }

                // Verifiy Password with Hash Password from Database
                bool verified = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);
                if (verified != true)
                {

                    // Change count failed login
                    user.LoginFailedCount += 1;

                    // Update user info
                    userRepository.Update(user);

                    // Apply change
                    unitOfWork.Commit();
                    return new Result<JwtLoginModel>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(
                            MessageUserConstant.WRONG_PASSWORD,
                            messageResult.Messages
                            )
                    };
                }
                JwtUtils jwtUtils = new JwtUtils();
                UserModel userModel = user.Adapt<UserModel>();
                string token = jwtUtils.GenerateJwtToken(userModel, configuration);

                // Object to return
                JwtLoginModel jwtModel = new JwtLoginModel()
                {
                    Token = token
                };
                // User login success
                user.IsLogin = true;

                // Save token
                user.Token = token;

                // Database update
                userRepository.Update(user);
                unitOfWork.Commit();

                return new Result<JwtLoginModel>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = jwtModel,
                    Message = MessageUtils.Message(
                            MessageUserConstant.LOG_IN_SUCCESS,
                            messageResult.Messages
                            )
                };
            }
            catch (Exception e)
            {
                return new Result<JwtLoginModel>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        public Result<bool> LogOut(string userName)
        {
            try
            {
                var user = userRepository.Get(u => u.UserName == userName);
                if (user == null)
                {
                    return new Result<bool>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(
                            MessageUserConstant.NOT_FOUND_USER,
                            messageResult.Messages
                            )
                    };
                }
                user.IsLogin = false;
                user.Token = "";

                unitOfWork.Commit();
                return new Result<bool>()
                {
                    Success = ResultConstant.SUCCESS,
                    Message = MessageUtils.Message(
                            MessageUserConstant.LOG_OUT_SUCCESS,
                            messageResult.Messages
                            )
                };
            }
            catch (Exception e)
            {
                return new Result<bool>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        public Result<UserModel> Register(UserRegistration userRegistration)
        {
            try
            {
                var role = roleRepository.GetById(userRegistration.RoleId);
                if (role == null)
                {
                    return null;
                }
                User user = new User()
                {
                    UserName = userRegistration.UserName,
                    FullName = userRegistration.FullName,
                    Phone = userRegistration.Phone,
                    IsLogin = false,
                    Address = userRegistration.Address,
                    Email = userRegistration.Email,
                    // Hash password
                    Password = BCrypt.Net.BCrypt.HashPassword(userRegistration.Password),
                    LoginFailedCount = 0,
                    RoleId = userRegistration.RoleId,
                    Status = UserStatusEnumeration.Pending.ToString(),
                };
                userRepository.Add(user);
                unitOfWork.Commit();
                User userTmp = userRepository.Get(u => u.UserName == user.UserName);
                return new Result<UserModel>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = userTmp.Adapt<UserModel>(),
                    Message = MessageUtils.Message(
                            MessageUserConstant.REGISTER_SUCCESS,
                            messageResult.Messages
                            )
                };
            }
            catch (Exception e)
            {
                return new Result<UserModel>()
                {
                    Success = ResultConstant.SUCCESS,
                    MessageError = e.Message
                };
            }
        }
    }
}
