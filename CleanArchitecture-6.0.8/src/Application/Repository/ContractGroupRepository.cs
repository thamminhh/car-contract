
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Contracts;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class ContractGroupRepository : IContractGroupRepository
    {
        private readonly ContractContext _contractContext;



        public ContractGroupRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        private static string changeIdContractIntoString(int? id)
        {
            if (id == null)
            {
                return null;
            }
            if (id == 1)
            {
                return ContractStatusConstant.ContractExportingName;
            }
            if (id == 2)
            {
                return ContractStatusConstant.ContractExportedName;
            }
            if (id == 3)
            {
                return ContractStatusConstant.ContractCancelledName;
            }
            return "";

        }

        public int GetNumberOfContracts(ContractFilter filter)
        {
            {
                IQueryable<ContractGroup> contracts = _contractContext.ContractGroups;

                if (filter != null)
                {
                    if (filter.ContractGroupStatusId.HasValue)
                    {
                        contracts = contracts.Where(c => c.ContractGroupStatusId == filter.ContractGroupStatusId);
                    }
                    if (filter.UserId.HasValue)
                    {
                        contracts = contracts.Where(c => c.UserId == filter.UserId);
                    }

                }
                return contracts.Count();
            }
        }
        public ContractGroupDataModel GetContractGroupById(int contractGroupId)
        {
            ContractGroup contractGroup = _contractContext.ContractGroups
                .Include(c => c.ContractFile)
                .FirstOrDefault(c => c.Id == contractGroupId);

            var customerInfo = _contractContext.CustomerInfos.Find(contractGroup.CustomerInfoId);
            var contractGroupStatus = _contractContext.ContractGroupStatuses.Find(contractGroup.ContractGroupStatusId);

            return new ContractGroupDataModel
            {
                Id = contractGroup.Id,
                UserId = contractGroup.UserId,
                CarId = contractGroup.CarId,
                RentPurpose = contractGroup.RentPurpose,
                RentFrom = contractGroup.RentFrom,
                RentTo = contractGroup.RentTo,
                RequireDescriptionInfoCarBrand = contractGroup.RequireDescriptionInfoCarBrand,
                RequireDescriptionInfoSeatNumber = contractGroup.RequireDescriptionInfoSeatNumber,
                RequireDescriptionInfoYearCreate = contractGroup.RequireDescriptionInfoYearCreate,
                RequireDescriptionInfoCarColor = contractGroup.RequireDescriptionInfoCarColor,
                DeliveryAddress = contractGroup.DeliveryAddress,

                ContractGroupStatusId = contractGroup.ContractGroupStatusId,
                ContractGroupStatusName = contractGroupStatus.Name,

                CustomerInfoId = contractGroup.CustomerInfoId,

                PhoneNumber = customerInfo.PhoneNumber,
                CustomerSocialInfoZalo = customerInfo.CustomerSocialInfoZalo,
                CustomerSocialInfoFacebook = customerInfo.CustomerSocialInfoFacebook,
                CustomerSocialInfoLinkedin = customerInfo.CustomerSocialInfoLinkedin,
                CustomerSocialInfoOther = customerInfo.CustomerSocialInfoOther,
                AddtionalInfo = customerInfo.AddtionalInfo,
                RelativeTel = customerInfo.RelativeTel,
                ExpertiseInfoIsFirstTimeRent = customerInfo.ExpertiseInfoIsFirstTimeRent,
                ExpertiseInfoTrustLevel = customerInfo.ExpertiseInfoTrustLevel,
                CustomerAddress = customerInfo.CustomerAddress,
                CustomerName = customerInfo.CustomerName,
                CompanyInfo = customerInfo.CompanyInfo,
                CitizenIdentificationInfoNumber = customerInfo.CitizenIdentificationInfoNumber,
                CitizenIdentificationInfoAddress = customerInfo.CitizenIdentificationInfoAddress,
                CitizenIdentificationInfoDateReceive = customerInfo.CitizenIdentificationInfoDateReceive,

                Path = contractGroup.ContractFile.Path,
                CitizenIdentifyImage1 = contractGroup.ContractFile.CitizenIdentifyImage1,
                CitizenIdentifyImage2 = contractGroup.ContractFile.CitizenIdentifyImage2,
                DrivingLisenceImage1 = contractGroup.ContractFile.DrivingLisenceImage1,
                DrivingLisenceImage2 = contractGroup.ContractFile.DrivingLisenceImage2,
                HousePaperImages = contractGroup.ContractFile.HousePaperImages,
                PassportImages = contractGroup.ContractFile.PassportImages,
                OtherImages = contractGroup.ContractFile.OtherImages,
                ExpertiseContracts = contractGroup.ContractFile.ExpertiseContracts,
                RentContracts = contractGroup.ContractFile.RentContracts,
                TransferContracts = contractGroup.ContractFile.TransferContracts,
                ReceiveContracts = contractGroup.ContractFile.ReceiveContracts,

            };

        }

        public ICollection<ContractGroupDataModel> GetContractGroups(int page, int pageSize, ContractFilter filter)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int skip = (page - 1) * pageSize;

            IQueryable<ContractGroup> contractGroups = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .Include(c => c.User)
                .Include(c => c.ContractGroupStatus)
                .Include(c => c.RentContract)
                .Include(c => c.TransferContract)
                .Include(c => c.ReceiveContract)
                .AsQueryable();

            if (filter != null)
            {
                if (filter.ContractGroupStatusId.HasValue)
                {
                    contractGroups = contractGroups.Where(c => c.ContractGroupStatusId == filter.ContractGroupStatusId);
                }
                if (filter.UserId.HasValue)
                {
                    contractGroups = contractGroups.Where(c => c.UserId == filter.UserId);
                }
            }

            var contractGroupDataModels = contractGroups
                .OrderBy(c => c.Id)
                .Skip(skip)
                .Take(pageSize)
                .Select(c => new ContractGroupDataModel
                {
                    Id = c.Id,
                    CustomerInfoId = c.CustomerInfoId,
                    CustomerName = c.CustomerInfo.CustomerName,
                    UserId = c.UserId,
                    StaffEmail = c.User.Email,
                    ContractGroupStatusId = c.ContractGroupStatusId,
                    ContractGroupStatusName = c.ContractGroupStatus.Name,

                    //ExpertiseContractId = c.ExpertiseContract != null? c.ExpertiseContract.Id : null,
                    //ExpertiseContractStatusId = c.ExpertiseContract != null ? c.ExpertiseContract.ContractStatusId: null,
                    //ExpertiseContractStatusName = c.ExpertiseContract != null ? changeIdContractIntoString(c.ExpertiseContract.ContractStatusId) : null,

                    RentContractId = c.RentContract != null ? c.RentContract.Id : null,
                    RentContractStatusId = c.RentContract != null ? c.RentContract.ContractStatusId : null,
                    RentContractStatusName = c.RentContract != null ? changeIdContractIntoString(c.RentContract.ContractStatusId) : null,

                    TransferContractId = c.TransferContract != null ? c.TransferContract.Id : null,
                    TransferContractStatusId = c.TransferContract != null ? c.TransferContract.ContractStatusId : null,
                    TransferContractStatusName = c.TransferContract != null ? changeIdContractIntoString(c.TransferContract.ContractStatusId) : null,

                    ReceiveContractId = c.ReceiveContract != null ? c.ReceiveContract.Id : null,
                    ReceiveContractStatusId = c.ReceiveContract != null ? c.ReceiveContract.ContractStatusId : null,
                    ReceiveContractStatusName = c.ReceiveContract != null ? changeIdContractIntoString(c.ReceiveContract.ContractStatusId) : null
                })
                .ToList();

            return contractGroupDataModels;
        }


        public bool ContractGroupExit(int id)
        {
            return _contractContext.ContractGroups.Any(c => c.Id == id);
        }
        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
        public void CreateContractGroup(ContractGroupCreateModel request)
        {
            var customerInfo = new CustomerInfo
            {
                PhoneNumber = request.PhoneNumber,
                CustomerName = request.CustomerName,
                CustomerSocialInfoZalo = request.CustomerSocialInfoZalo,
                CustomerSocialInfoFacebook = request.CustomerSocialInfoFacebook,
                CustomerSocialInfoLinkedin = request.CustomerSocialInfoLinkedin,
                CustomerSocialInfoOther = request.CustomerSocialInfoOther,
                AddtionalInfo = request.AddtionalInfo,
                RelativeTel = request.RelativeTel,
                ExpertiseInfoIsFirstTimeRent = request.ExpertiseInfoIsFirstTimeRent,
                ExpertiseInfoTrustLevel = request.ExpertiseInfoTrustLevel,
                CompanyInfo = request.CompanyInfo,
                CustomerAddress = request.CustomerAddress,
                CitizenIdentificationInfoNumber = request.CitizenIdentificationInfoNumber,
                CitizenIdentificationInfoAddress = request.CitizenIdentificationInfoAddress,
                CitizenIdentificationInfoDateReceive = request.CitizenIdentificationInfoDateReceive,

            };
            // Save the new ContractGroupGenerallInfos object to the database
            _contractContext.CustomerInfos.Add(customerInfo);
            _contractContext.SaveChanges();
            // Create new ContractGroup object and set its properties

            int contractGroupIdDefault = Constant.ContractGroupConstant.ContractGroupNotExpertised;
            var contractGroup = new ContractGroup
            {
                CustomerInfoId = customerInfo.Id,
                UserId = request.UserId,
                RentPurpose = request.RentPurpose,
                RentFrom = request.RentFrom,
                RentTo = request.RentTo,
                RequireDescriptionInfoCarBrand = request.RequireDescriptionInfoCarBrand,
                RequireDescriptionInfoSeatNumber = request.RequireDescriptionInfoSeatNumber,
                RequireDescriptionInfoYearCreate = request.RequireDescriptionInfoYearCreate,
                RequireDescriptionInfoCarColor = request.RequireDescriptionInfoCarColor,
                ContractGroupStatusId = contractGroupIdDefault,
                DeliveryAddress = request.CustomerAddress,
            };

            // Save the new ContractGroup to the database
            _contractContext.ContractGroups.Add(contractGroup);
            _contractContext.SaveChanges();

            // Create new ContractGroupStates object and set its properties
            var contractFile = new ContractFile
            {
                ContractGroupId = contractGroup.Id,
                CitizenIdentifyImage1 = request.CitizenIdentifyImage1,
                CitizenIdentifyImage2 = request.CitizenIdentifyImage2,
                DrivingLisenceImage1 = request.DrivingLisenceImage1,
                DrivingLisenceImage2 = request.DrivingLisenceImage2,
                HousePaperImages = request.HousePaperImages,
                PassportImages = request.PassportImages,
                OtherImages = request.OtherImages,
                CreateDate = request.CreateDate
            };

            // Save the new ContractGroupStates object to the database
            _contractContext.ContractFiles.Add(contractFile);
            _contractContext.SaveChanges();
        }

        public void UpdateContractGroup(int id, ContractGroupUpdateModel request)
        {
            // Retrieve the ContractGroup object from the database
            var ContractGroup = _contractContext.ContractGroups.Find(id);

            // Update the properties of the ContractGroup object
            ContractGroup.CustomerInfoId = request.CustomerInfoId;
            ContractGroup.UserId = request.UserId;
            ContractGroup.CarId = request.CarId;
            ContractGroup.RentPurpose = request.RentPurpose;
            ContractGroup.RentFrom = request.RentFrom;
            ContractGroup.RentTo = request.RentTo;
            ContractGroup.RequireDescriptionInfoCarBrand = request.RequireDescriptionInfoCarBrand;
            ContractGroup.RequireDescriptionInfoSeatNumber = request.RequireDescriptionInfoSeatNumber;
            ContractGroup.RequireDescriptionInfoYearCreate = request.RequireDescriptionInfoYearCreate;
            ContractGroup.RequireDescriptionInfoCarColor = request.RequireDescriptionInfoCarColor;
            ContractGroup.ContractGroupStatusId = request.ContractGroupStatusId;


            // Save the changes to the database
            _contractContext.ContractGroups.Update(ContractGroup);
            _contractContext.SaveChanges();

            // Retrieve the ContractGroupGenerallInfos object from the database
            var customerInfo = _contractContext.CustomerInfos.Find(request.CustomerInfoId);

            // Update the properties of the ContractGroupGenerallInfos object
            customerInfo.CustomerName = request.CustomerName;
            customerInfo.PhoneNumber = request.PhoneNumber;
            customerInfo.CustomerSocialInfoZalo = request.CustomerSocialInfoZalo;
            customerInfo.CustomerSocialInfoFacebook = request.CustomerSocialInfoFacebook;
            customerInfo.CustomerSocialInfoLinkedin = request.CustomerSocialInfoLinkedin;
            customerInfo.CustomerSocialInfoOther = request.CustomerSocialInfoOther;
            customerInfo.AddtionalInfo = request.AddtionalInfo;
            customerInfo.RelativeTel = request.RelativeTel;
            customerInfo.ExpertiseInfoIsFirstTimeRent = request.ExpertiseInfoIsFirstTimeRent;
            customerInfo.ExpertiseInfoTrustLevel = request.ExpertiseInfoTrustLevel;
            customerInfo.CompanyInfo = request.CompanyInfo;
            customerInfo.CustomerAddress = request.CustomerAddress;
            customerInfo.CitizenIdentificationInfoNumber = request.CitizenIdentificationInfoNumber;
            customerInfo.CitizenIdentificationInfoAddress = request.CitizenIdentificationInfoAddress;
            customerInfo.CitizenIdentificationInfoDateReceive = request.CitizenIdentificationInfoDateReceive;


            // Save the changes to the database
            _contractContext.CustomerInfos.Update(customerInfo);
            _contractContext.SaveChanges();

            // Retrieve the ContractGroupStates object from the database
            var contractFile = _contractContext.ContractFiles.Where(c => c.ContractGroupId == id).FirstOrDefault();

            // Update the properties of the ContractGroupStates object
            contractFile.Path = request.Path;
            contractFile.CitizenIdentifyImage1 = request.CitizenIdentifyImage1;
            contractFile.CitizenIdentifyImage2 = request.CitizenIdentifyImage2;
            contractFile.DrivingLisenceImage1 = request.DrivingLisenceImage1;
            contractFile.DrivingLisenceImage2 = request.DrivingLisenceImage2;
            contractFile.HousePaperImages = request.HousePaperImages;
            contractFile.PassportImages = request.PassportImages;
            contractFile.OtherImages = request.OtherImages;
            contractFile.ExpertiseContracts = request.ExpertiseContracts;
            contractFile.RentContracts = request.RentContracts;
            contractFile.TransferContracts = request.TransferContracts;
            contractFile.ReceiveContracts = request.ReceiveContracts;

            // Save the changes to the database
            _contractContext.ContractFiles.Update(contractFile);
            _contractContext.SaveChanges();
        }

        public bool UpdateContractGroupStatus(int id, ContractGroupUpdateStatusModel request)
        {
            var ContractGroup = _contractContext.ContractGroups.Where(c => c.Id == id).FirstOrDefault();
            if (ContractGroup == null)
                return false;

            ContractGroup.ContractGroupStatusId = request.ContractGroupStatusId;
            return Save();

        }

        public bool DeleteContractGroup(int id)
        {
            var ContractGroup = _contractContext.ContractGroups.Where(c => c.Id == id).FirstOrDefault();
            if (ContractGroup == null)
                return false;

            ContractGroup.ContractGroupStatusId = 5;
            return Save();

        }

        public bool UpdateContractCarId(int id, int? carId)
        {
            var ContractGroup = _contractContext.ContractGroups.Where(c => c.Id == id).FirstOrDefault();
            if (ContractGroup == null)
                return false;

            ContractGroup.CarId = carId;
            return Save();
        }

        //public ICollection<ContractGroupDataModel> GetContractGroups(int page, int pageSize, ContractFilter filter)
        //{
        //    if (page < 1)
        //    {
        //        page = 1;
        //    }

        //    if (pageSize < 1)
        //    {
        //        pageSize = 10;
        //    }
        //    int skip = (page - 1) * pageSize;
        //    IQueryable<ContractGroup> contractGroups = _contractContext.ContractGroups.AsQueryable();

        //    if (filter != null)
        //    {
        //        if (filter.ContractStatusId.HasValue)
        //        {
        //            contractGroups = contractGroups.Where(c => c.ContractGroupStatusId == filter.ContractStatusId);
        //        }
        //    }

        //    return (from c in contractGroups
        //            join ci in _contractContext.CustomerInfos on c.CustomerInfoId equals ci.Id
        //            join u in _contractContext.Users on c.UserId equals u.Id
        //            join cgt in _contractContext.ContractGroupStatuses on c.ContractGroupStatusId equals cgt.Id

        //            join ec in _contractContext.ExpertiseContracts on c.Id equals ec.ContractGroupId into expertiseContracts
        //            from ec in expertiseContracts.DefaultIfEmpty()

        //            join rc in _contractContext.RentContracts on c.Id equals rc.ContractGroupId into rentContracts
        //            from rc in rentContracts.DefaultIfEmpty()

        //            join tc in _contractContext.TransferContracts on c.Id equals tc.ContractGroupId into transferContracts
        //            from tc in transferContracts.DefaultIfEmpty()

        //            join rvc in _contractContext.ReceiveContracts on c.Id equals rvc.ContractGroupId into receiveContracts
        //            from rvc in receiveContracts.DefaultIfEmpty()

        //            select new ContractGroupDataModel
        //            {
        //                Id = c.Id,
        //                CustomerInfoId = ci.Id,
        //                CustomerName = ci.CustomerName,
        //                UserId = u.Id,
        //                StaffEmail = u.Email,
        //                ContractGroupStatusId = c.ContractGroupStatusId,
        //                ContractGroupStatusName = cgt.Name,

        //                ExpertiseContractId = ec != null ? ec.Id : null,
        //                ExpertiseContractStatusId = ec != null ? ec.ContractStatusId : null,
        //                ExpertiseContractStatusName = ec != null ? changeIdContractIntoString(ec.Id) : null,

        //                RentContractId = rc != null ? rc.Id : null,
        //                RentContractStatusId = rc != null ? rc.ContractStatusId : null,
        //                RentContractStatusName = rc != null ? changeIdContractIntoString(rc.Id) : null,

        //                TransferContractId = tc != null ? tc.Id : null,
        //                TransferContractStatusId = tc != null ? tc.ContractStatusId : null,
        //                TransferContractStatusName = tc != null ? (string)changeIdContractIntoString(tc.Id) : null,

        //                ReceiveContractId = rvc != null ? rvc.Id : null,
        //                ReceiveContractStatusId = rvc != null ? rvc.ContractStatusId : null,
        //                ReceiveContractStatusName = ec != null ? changeIdContractIntoString(rvc.Id) : null,

        //            }).OrderBy(c => c.Id).Skip(skip).Take(pageSize).ToList();
        //}


    }
}
