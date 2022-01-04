using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
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
    public class ConsignmentService : IConsignmentService
    {
        private readonly IConsignmentRepository consignmentRepository;
        private readonly IDrugRepository drugRepository;
        private readonly IConsignmentDrugRepository consignmentDrugRepository;
        private readonly IPharmacyRepository pharmacyRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly MessageResult pharmacyMessageResult;
        private readonly MessageResult consignmentMessageResult;

        public ConsignmentService(
            IConsignmentRepository consignmentRepository,
            IDrugRepository drugRepository,
            IConsignmentDrugRepository consignmentDrugRepository,
            IPharmacyRepository pharmacyRepository,
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache)
        {
            this.consignmentRepository = consignmentRepository;
            this.drugRepository = drugRepository;
            this.consignmentDrugRepository = consignmentDrugRepository;
            this.pharmacyRepository = pharmacyRepository;
            this.unitOfWork = unitOfWork;
            pharmacyMessageResult = memoryCache.GetOrCreate(CacheMessageConstant.PHARMACY, _ =>
            {
                return CacheEngine.GetMessages(MessageDomainConstant.PHARMACY);
            });
            consignmentMessageResult = memoryCache.GetOrCreate(CacheMessageConstant.CONSIGNMENT, _ =>
            {
                return CacheEngine.GetMessages(MessageDomainConstant.CONSIGNMENT);
            });

        }

        public Result<List<ConsignmentModel>> GetConsignments(
            int pharmacyId,
            DateTime? specificDate,
            int? year,
            int? month,
            int? day)
        {
            try
            {
                // Check pharmacy is existed?
                var pharmacy = pharmacyRepository.Get(p => p.PharmacyId == pharmacyId);
                if (pharmacy == null)
                {
                    return new Result<List<ConsignmentModel>>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(MessagePharmacyConstant.NOT_EXISTED, pharmacyMessageResult.Messages)
                    };
                }

                List<Consignment> consignments = null;
                // If not pass param. Return all consignment in pharmacy
                if (specificDate == null && year == null && month == null && day == null)
                {
                    consignments = consignmentRepository.GetAll().ToList();
                }
                // If have specific date. Get all consignment by specific date
                else if (specificDate != null || (year != null && month != null && day != null))
                {
                    consignments = consignmentRepository.GetMany(c =>
                        c.DateImported.Day == specificDate.Value.Day
                        && c.DateImported.Month == specificDate.Value.Month
                        && c.DateImported.Year == specificDate.Value.Year).ToList();
                }
                // If pass year only
                else if (year != null && (month == null || day == null))
                {
                    consignments = consignmentRepository.GetMany(c => c.DateImported.Year == year).ToList();
                }
                // If pass year and month
                else if (year != null && month != null && day == null)
                {
                    consignments = consignmentRepository.GetMany(c =>
                        c.DateImported.Year == year
                        && c.DateImported.Month == month).ToList();
                }
                // If pass year and day
                else if (year != null && month == null && day != null)
                {

                }
                // If pass month
                else if (month != null && (year == null || day == null))
                {

                }
                // If pass month and day
                else if (month != null && day != null && year == null)
                {

                }
                // if pass day
                else if (day != null && month == null && year == null)
                {

                }

                // Model to return for end user
                List<ConsignmentModel> consignmentModels = new List<ConsignmentModel>();

                consignments.ForEach(c =>
                {
                    // Get all ConsignmentDrugs by ConsignmentId
                    var consignmentDrugs = consignmentDrugRepository
                        .GetMany(cd => cd.ConsignmentId == c.ConsignmentId).ToList();
                    List<ConsignmentModel.DrugLocal> drugs = new List<ConsignmentModel.DrugLocal>();

                    // Convert Drug(DB) model to DrugLocal model
                    consignmentDrugs.ForEach(cds =>
                    {
                        var drug = drugRepository.Get(d => d.DrugId == cds.DrugId);
                        drugs.Add(drug.Adapt<ConsignmentModel.DrugLocal>());
                    });

                    consignmentModels.Add(new ConsignmentModel()
                    {
                        ConsignmentId = c.ConsignmentId,
                        DateImported = c.DateImported,
                        From = c.From,
                        TotalCost = c.TotalCost,
                        Drugs = drugs
                    });

                });
                return new Result<List<ConsignmentModel>>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = consignmentModels,
                    Message = MessageUtils.Message(MessageConsignmentConstant.GET_ALL_SUCCESS, consignmentMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<List<ConsignmentModel>>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }

        public Result<ConsignmentImported> ImportConsignment(int pharmacyId, ConsignmentImported consignmentImported)
        {
            try
            {
                // Check pharmacy is existed?
                var pharmacy = pharmacyRepository.Get(p => p.PharmacyId == pharmacyId);
                if (pharmacy == null)
                {
                    return new Result<ConsignmentImported>()
                    {
                        Success = ResultConstant.FAILED,
                        Client = ResultConstant.CLIENT,
                        Message = MessageUtils.Message(MessagePharmacyConstant.NOT_EXISTED, pharmacyMessageResult.Messages)
                    };
                }
                // Adapt data to Consignment object
                Consignment consignment = new Consignment()
                {
                    DateImported = consignmentImported.DateImported,
                    From = consignmentImported.From,
                    PharmacyId = pharmacyId
                };
                consignmentRepository.Add(consignment);
                // Save consignment to database
                unitOfWork.Commit();

                decimal totalCost = 0;

                // Update drugs existed
                if (consignmentImported.DrugExisteds != null)
                {
                    ConsignmentDrug consignmentDrug;
                    Drug drug;
                    consignmentImported.DrugExisteds.ForEach(_ =>
                    {
                        consignmentDrug = new ConsignmentDrug()
                        {
                            ConsignmentId = consignment.ConsignmentId,
                            DrugId = _.DrugId,
                            Quantity = _.Quantity,
                            Cost = _.Cost,
                        };

                        // Total of cost for consignment
                        totalCost += _.Quantity * _.Cost;
                        consignmentDrugRepository.Add(consignmentDrug);

                        // Update quantity Drug
                        drug = drugRepository.Get(d => d.DrugId == _.DrugId);
                        drug.Quantity += _.Quantity;
                    });
                }

                // Create new drugs if not exist
                if (consignmentImported.DrugNews != null)
                {
                    // Create Drug
                    List<Drug> drugs = new List<Drug>();
                    consignmentImported.DrugNews.ForEach(_ =>
                    {
                        drugs.Add(_.Adapt<Drug>());
                    });
                    drugs.ForEach(d =>
                    {
                        drugRepository.Add(d);
                    });
                    // Save drug to db
                    unitOfWork.Commit();

                    ConsignmentDrug consignmentDrug;

                    for (int i = 0; i < consignmentImported.DrugNews.Count; i++)
                    {
                        consignmentDrug = new ConsignmentDrug()
                        {
                            ConsignmentId = consignment.ConsignmentId,
                            DrugId = drugs[i].DrugId,
                            Quantity = consignmentImported.DrugNews[i].Quantity,
                            Cost = consignmentImported.DrugNews[i].Cost
                        };
                        totalCost += consignmentImported.DrugNews[i].Quantity * consignmentImported.DrugNews[i].Cost;
                        consignmentDrugRepository.Add(consignmentDrug);
                    }
                }

                // Update total cost of consignment
                consignment.TotalCost = totalCost;

                // Save to database
                unitOfWork.Commit();

                return new Result<ConsignmentImported>()
                {
                    Success = ResultConstant.SUCCESS,
                    Data = consignmentImported,
                    Message = MessageUtils.Message(MessageConsignmentConstant.CREATE_SUCCESS, consignmentMessageResult.Messages)
                };
            }
            catch (Exception e)
            {
                return new Result<ConsignmentImported>()
                {
                    Success = ResultConstant.FAILED,
                    MessageError = e.Message
                };
            }
        }
    }
}
