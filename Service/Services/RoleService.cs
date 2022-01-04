using System;
using System.Collections.Generic;
using System.Linq;
using EFMC.Data.Entities;
using EFMC.Data.Interfaces;
using EFMC.Service.Common;
using EFMC.Service.Common.Constants;
using EFMC.Service.Common.Results;
using EFMC.Service.Common.Utils;
using EFMC.Service.Interfaces;
using EFMC.Service.Models;
using Mapster;
using Microsoft.Extensions.Caching.Memory;

namespace EFMC.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly MessageResult messageResult;

        public RoleService(
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
            messageResult = memoryCache.GetOrCreate(
                CacheMessageConstant.ROLE,
                roleMessage =>
            {
                return CacheEngine.GetMessages(MessageDomainConstant.ROLE);
            });
        }

        public Result<Role> AddRole(RoleCreation roleCreation)
        {
            try
            {
                Role role = new Role()
                {
                    RoleName = roleCreation.RoleName
                };
                roleRepository.Add(role);
                // Save new role to database
                unitOfWork.Commit();

                return new Result<Role>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = role,
                    Message = MessageUtils.Message(
                        MessageRoleConstant.ADD_ROLE_SUCCESS,
                        messageResult.Messages
                        )
                };
            }
            catch (Exception e)
            {
                return new Result<Role>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        public Result<List<RoleInfo>> GetRoles()
        {
            try
            {
                List<RoleInfo> roles = roleRepository
                    .GetAll()
                    .ToList()
                    .ConvertAll(new Converter<Role, RoleInfo>(RoleInfo.ToRoleInfo)); // Convert List<Role> to List<RoleInfo>
                return new Result<List<RoleInfo>>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = roles,
                    Message = MessageUtils.Message(
                        MessageRoleConstant.GET_ROLES_SUCCESS,
                        messageResult.Messages
                        )
                };
            }
            catch (Exception e)
            {
                return new Result<List<RoleInfo>>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }
    }
}
