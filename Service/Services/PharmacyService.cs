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

namespace EFMC.Service.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IPharmacyRepository pharmacyRepository;
        private readonly IUserPharmacyRepository userPharmacyRepository;
        private readonly IUserRepository userRepository;
        private readonly IIndustryRepository industryRepository;
        private readonly IPharmacyIndustryRepository pharmacyIndustryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly MessageResult userMessageResult;
        private readonly MessageResult pharmacyMessageResult;
        private readonly MessageResult industryMessageResult;

        public PharmacyService(
            IPharmacyRepository pharmacyRepository,
            IUserPharmacyRepository userPharmacyRepository,
            IUserRepository userRepository,
            IIndustryRepository industryRepository,
            IPharmacyIndustryRepository pharmacyIndustryRepository,
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache
            )
        {
            this.pharmacyRepository = pharmacyRepository;
            this.userPharmacyRepository = userPharmacyRepository;
            this.userRepository = userRepository;
            this.industryRepository = industryRepository;
            this.pharmacyIndustryRepository = pharmacyIndustryRepository;
            this.unitOfWork = unitOfWork;
            userMessageResult = memoryCache.GetOrCreate(
                CacheMessageConstant.USER,
                userMessage =>
                {
                    return CacheEngine.GetMessages(MessageDomainConstant.USER);
                });
            pharmacyMessageResult = memoryCache.GetOrCreate(
                CacheMessageConstant.PHARMACY,
                pharmacyMessage =>
                {
                    return CacheEngine.GetMessages(MessageDomainConstant.PHARMACY);
                });
            industryMessageResult = memoryCache.GetOrCreate(
                CacheMessageConstant.INDUSTRY,
                industryMessage =>
                {
                    return CacheEngine.GetMessages(MessageDomainConstant.INDUSTRY);
                });
        }

        public Result<List<IndustryModel>> CreateIndustry(int pharmacyId, List<string> industries)
        {
            try
            {
                // Check pharmacy is existed?
                var pharmacy = pharmacyRepository.Get(p => p.PharmacyId == pharmacyId);
                if (pharmacy == null)
                {
                    return new Result<List<IndustryModel>>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(MessagePharmacyConstant.NOT_EXISTED, pharmacyMessageResult.Messages)
                    };
                }
                // Convert List<string> to List<Industry>
                List<Industry> industriesList = industries.ConvertAll(new Converter<string, Industry>(IndustryModel.ToIndustry));

                industriesList.ForEach(i => industryRepository.Add(i));

                // Save industries to db
                unitOfWork.Commit();

                // Update PharmacyIndustry
                PharmacyIndustry pharmacyIndustry;
                industriesList.ForEach(i =>
                {
                    pharmacyIndustry = new PharmacyIndustry()
                    {
                        PharmacyId = pharmacyId,
                        IndustryId = i.IndustryId
                    };
                    pharmacyIndustryRepository.Add(pharmacyIndustry);
                });
                // Save PharmacyIndustry to db
                unitOfWork.Commit();


                return new Result<List<IndustryModel>>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = industriesList.ConvertAll(new Converter<Industry, IndustryModel>(IndustryModel.ToIndustryModel)),
                    Message = MessageUtils.Message(MessageIndustryConstant.CREATE_SUCCESS, industryMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<List<IndustryModel>>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        // Create new pharmacy
        public Result<PharmacyModel> CreatePharmacy(PharmacyCreationModel pharmacy)
        {
            try
            {
                // Check user is existed?
                var user = userRepository.Get(u => u.UserId == pharmacy.UserId);
                if (user == null)
                {
                    return new Result<PharmacyModel>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Data = null,
                        Message = MessageUtils.Message(MessageUserConstant.NOT_FOUND_USER, userMessageResult.Messages)
                    };
                }

                // Assign properties to object repository
                Pharmacy pharmacyRepo = new Pharmacy()
                {
                    Name = pharmacy.Name,
                    Address = pharmacy.Address,
                    DateCreated = DateTime.Now,
                    Status = PharmacyEnumeration.Activate.ToString()
                };
                pharmacyRepository.Add(pharmacyRepo);
                // Save pharmacy to database
                unitOfWork.Commit();

                List<User> users = new List<User>();
                if (pharmacy.PharmacistGeneration != null)
                {
                    for (int i = 0; i < pharmacy.PharmacistGeneration.Count; i++)
                    {
                        users.Add(new User()
                        {
                            UserName = $"{pharmacy.PharmacistGeneration.UserNameFormat}{i}",
                            // Hash password
                            Password = BCrypt.Net.BCrypt.HashPassword(UserConstant.PASSWORD_DEFAULT),
                            IsLogin = false,
                            LoginFailedCount = 0,
                            Phone = "",
                            FullName = $"{pharmacy.PharmacistGeneration.UserNameFormat}",
                            RoleId = 2,
                            Status = UserStatusEnumeration.Activate.ToString()
                        });
                    }
                    users.ForEach(user =>
                    {
                        userRepository.Add(user);
                    });
                    unitOfWork.Commit();
                }

                if (users.Count > 0)
                {
                    var tmpUser = userRepository.Get(user => user.UserName.Contains($"{pharmacy.PharmacistGeneration.UserNameFormat}%"));
                    if (tmpUser != null)
                    {
                        return new Result<PharmacyModel>()
                        {
                            Success = ResultConstant.FAILED,
                            Client = ResultConstant.CLIENT,
                            Data = null,
                            Message = MessageUtils.Message(MessagePharmacyConstant.IS_EXISTED_USER, userMessageResult.Messages)
                        };
                    }
                    // Add to UserPharmacy entity
                    users.ForEach(pharmacist => userPharmacyRepository.Add(new UserPharmacy()
                    {
                        OwnerId = pharmacy.UserId,
                        PharmacyId = pharmacyRepo.PharmacyId,
                        PharmacistId = pharmacist.UserId,
                        Status = UserPharmacyEnumeration.Activate.ToString()
                    }));
                    // Save userPharmacy to database
                    unitOfWork.Commit();
                }
                // If have not any pharmacist. Owner is pharmacist
                else
                {
                    UserPharmacy userPharmacy = new UserPharmacy()
                    {
                        OwnerId = pharmacy.UserId,
                        PharmacyId = pharmacyRepo.PharmacyId,
                        PharmacistId = pharmacy.UserId,
                        Status = UserPharmacyEnumeration.Activate.ToString()
                    };
                    userPharmacyRepository.Add(userPharmacy);
                    // Save userPharmacy to database
                    unitOfWork.Commit();
                }

                return new Result<PharmacyModel>()
                {
                    Success = ResultConstant.SUCCESS,
                    Client = ResultConstant.CLIENT,
                    Data = null,
                    Message = MessageUtils.Message(MessagePharmacyConstant.CREATE_SUCCESS, userMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<PharmacyModel>()
                {
                    Success = ResultConstant.FAILED,
                    Data = null,
                    MessageError = e.Message
                };
            }
        }

        public Result<List<IndustryModel>> GetIndustries(int pharmacyId)
        {
            try
            {
                // Check pharmacy is existed?
                var pharmacy = pharmacyRepository.Get(p => p.PharmacyId == pharmacyId);
                if (pharmacy == null)
                {
                    return new Result<List<IndustryModel>>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(MessagePharmacyConstant.NOT_EXISTED, pharmacyMessageResult.Messages)
                    };
                };
                List<PharmacyIndustry> pharmacyIndustries = pharmacyIndustryRepository.GetMany(pharmacyIndustry => pharmacyIndustry.PharmacyId == pharmacyId).ToList();
                List<IndustryModel> industryModels = new List<IndustryModel>();
                Industry industry;
                pharmacyIndustries.ForEach(pharmacyIndustry =>
                {
                    industry = industryRepository.Get(i => i.IndustryId == pharmacyIndustry.IndustryId);
                    industryModels.Add(industry.Adapt<IndustryModel>());
                });
                return new Result<List<IndustryModel>>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = industryModels,
                    Message = MessageUtils.Message(MessageIndustryConstant.GET_ALL_SUCCESS, industryMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<List<IndustryModel>>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        // Get all pharmacies by OwnerId
#nullable enable
        public Result<List<PharmacyModel>> GetPharmacies(int ownerId, string? status)
        {
            try
            {
                UserPharmacy owner;
                if (status == null)
                {
                    owner = userPharmacyRepository.Get(o => o.OwnerId == ownerId);
                }
                else
                {
                    owner = userPharmacyRepository.Get(o => o.OwnerId == ownerId);

                    if (owner == null)
                    {
                        return new Result<List<PharmacyModel>>()
                        {
                            Success = ResultConstant.FAILED,
                            Client = ResultConstant.CLIENT,
                            Message = MessageUtils.Message(MessageUserConstant.NOT_FOUND_USER, userMessageResult.Messages)
                        };
                    }
                    else
                    {
                        if (!Enum.IsDefined(typeof(UserPharmacyEnumeration), status))
                        {
                            return new Result<List<PharmacyModel>>()
                            {
                                Success = ResultConstant.FAILED,
                                Client = ResultConstant.CLIENT,
                                Message = MessageUtils.Message(MessageUserConstant.INVALID_STATUS, userMessageResult.Messages)
                            };
                        }
                        owner = userPharmacyRepository.Get(o => o.OwnerId == ownerId && o.Status == status);
                        if (owner == null)
                        {
                            return new Result<List<PharmacyModel>>()
                            {
                                Success = ResultConstant.FAILED,
                                Client = ResultConstant.CLIENT,
                                Message = MessageUtils.Message(MessageUserConstant.NOT_FOUND_STATUS, userMessageResult.Messages)
                            };
                        }

                    }
                }
                // Get all UserPharmacy with OwnerId
                // Then group by PharmacyId
                var userPharmacies = userPharmacyRepository
                    .GetMany(up => up.OwnerId == ownerId).ToList()
                    .GroupBy(userPharmacy => userPharmacy.PharmacyId).ToList();

                // Convert data to list of PharmacyModel
                List<PharmacyModel> pharmacyModels = new List<PharmacyModel>();
                userPharmacies.ForEach(userPharmacyGroup =>
                {
                    // Get pharmacy's information by pharmacyId(Key)
                    var pharmacy = pharmacyRepository.Get(p => p.PharmacyId == userPharmacyGroup.Key);
                    List<PharmacyModel.UserPharmacyLocal> userPharmacyLocals = pharmacy.UserPharmacies.ToList().ConvertAll(
                            new Converter<UserPharmacy, PharmacyModel.UserPharmacyLocal>(PharmacyModel.ToUserPharmacyLocal)
                        );
                    // Get name user by id
                    foreach (PharmacyModel.UserPharmacyLocal userPharmacyLocal in userPharmacyLocals)
                    {
                        var ownerTemp = userRepository.Get(u => u.UserId == userPharmacyLocal.OwnerId);
                        var pharmacistTemp = userRepository.Get(u => u.UserId == userPharmacyLocal.PharmacistId);

                        userPharmacyLocal.OwnerName = ownerTemp.FullName;
                        userPharmacyLocal.PharmacisName = pharmacistTemp.FullName;
                    }
                    PharmacyModel pharmacyModel = new PharmacyModel()
                    {
                        PharmacyId = pharmacy.PharmacyId,
                        Address = pharmacy.Address,
                        Name = pharmacy.Name,
                        DateCreated = pharmacy.DateCreated == null ? new DateTime() : (DateTime)pharmacy.DateCreated,
                        Status = pharmacy.Status,
                        // Convert UserPharmacy model to UserPharmacyLocal model
                        UserPharmacies = userPharmacyLocals
                    };
                    // Add PharmacyModel to PharmacyModel's list
                    pharmacyModels.Add(pharmacyModel);
                });
                // Success
                return new Result<List<PharmacyModel>>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = pharmacyModels,
                    Message = MessageUtils.Message(MessagePharmacyConstant.GET_PHARMACIES_SUCCESS, pharmacyMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<List<PharmacyModel>>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        public Result<PharmacyModel> UpdatePharmacy(int pharmacyId, PharmacyUpdateModel pharmacyUpdate)
        {
            try
            {
                // Check pharmacy is existed?
                var pharmacy = pharmacyRepository.Get(p => p.PharmacyId == pharmacyId);
                if (pharmacy == null)
                {
                    return new Result<PharmacyModel>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(MessagePharmacyConstant.NOT_EXISTED, pharmacyMessageResult.Messages)
                    };
                }
                pharmacy.Name = pharmacyUpdate.Name;
                pharmacy.Address = pharmacyUpdate.Address;
                pharmacy.Status = pharmacyUpdate.Status;

                pharmacyUpdate.UserPharmacies.ForEach(pharmacyUpdateTemp =>
                {
                    var pharmacyTemp = userPharmacyRepository.Get(up => up.UserPharmacyId == pharmacyUpdateTemp.UserPharmacyId);
                    if (pharmacyTemp != null)
                    {
                        pharmacyTemp.OwnerId = (int)pharmacyUpdateTemp.OwnerId;
                        pharmacyTemp.PharmacistId = (int)pharmacyUpdateTemp.PharmacistId;
                        pharmacyTemp.Status = pharmacyUpdate.Status;
                    }
                });

                // Save to database
                unitOfWork.Commit();



                return new Result<PharmacyModel>()
                {
                    Success = ResultConstant.SUCCESS,
                    Message = MessageUtils.Message(MessagePharmacyConstant.NOT_EXISTED, pharmacyMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<PharmacyModel>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }

        }
    }
}

