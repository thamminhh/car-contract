
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

                Path = contractGroup.ContractFile.Path,
                CitizenIdentifyImage1 = contractGroup.ContractFile.CitizenIdentifyImage1,
                CitizenIdentifyImage2 = contractGroup.ContractFile.CitizenIdentifyImage2,
                DrivingLisenceImage1 = contractGroup.ContractFile.DrivingLisenceImage1,
                DrivingLisenceImage2 = contractGroup.ContractFile.DrivingLisenceImage2,
                HousePaperImages = contractGroup.ContractFile.HousePaperImages,
                PassportImages = contractGroup.ContractFile.PassportImages,
                OtherImages = contractGroup.ContractFile.OtherImages,
                RentContracts = contractGroup.ContractFile.RentContracts,
                TransferContracts = contractGroup.ContractFile.TransferContracts
            };


            //var ContractGroup = (from c in _contractContext.ContractGroups
            //                     join ci in _contractContext.CustomerInfos on c.CustomerInfoId equals ci.Id
            //                     join cf in _contractContext.ContractFiles on c.Id equals cf.Id
            //                     where c.Id == ContractGroupId
            //                     select new
            //                     {
            //                         c.Id,
            //                         c.CustomerInfoId,
            //                         c.UserId,
            //                         c.CarId,
            //                         c.RentPurpose,
            //                         c.RentFrom,
            //                         c.RentTo,
            //                         c.RequireDescriptionInfoCarBrand,
            //                         c.RequireDescriptionInfoSeatNumber,
            //                         c.RequireDescriptionInfoYearCreate,
            //                         c.RequireDescriptionInfoCarColor,
            //                         c.ContractGroupStatus,

            //                         ci.PhoneNumber,
            //                         ci.CustomerSocialInfoZalo,
            //                         ci.CustomerSocialInfoFacebook,
            //                         ci.CustomerSocialInfoLinkedin,
            //                         ci.CustomerSocialInfoOther,
            //                         ci.AddtionalInfo,
            //                         ci.RelativeTel,
            //                         ci.ExpertiseInfoIsFirstTimeRent,
            //                         ci.ExpertiseInfoTrustLevel,
            //                         ci.CompanyInfo,

            //                         cf.Path,
            //                         cf.CitizenIdentifyImage1,
            //                         cf.CitizenIdentifyImage2,
            //                         cf.DrivingLisenceImage1,
            //                         cf.DrivingLisenceImage2,
            //                         cf.HousePaperImages,
            //                         cf.PassportImages,
            //                         cf.OtherImages,
            //                         cf.RentContracts,
            //                         cf.TransferContracts,
            //                         cf.CreateDate
            //                     }).FirstOrDefault();
            //return ContractGroup;

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
                .Include(c => c.ExpertiseContract)
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

                    ExpertiseContractId = c.ExpertiseContract != null? c.ExpertiseContract.Id : null,
                    ExpertiseContractStatusId = c.ExpertiseContract != null ? c.ExpertiseContract.ContractStatusId: null,
                    ExpertiseContractStatusName = c.ExpertiseContract != null ? changeIdContractIntoString(c.ExpertiseContract.ContractStatusId) : null,

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
                        CustomerSocialInfoZalo = request.CustomerSocialInfoZalo,
                        CustomerSocialInfoFacebook = request.CustomerSocialInfoFacebook,
                        CustomerSocialInfoLinkedin = request.CustomerSocialInfoLinkedin,
                        CustomerSocialInfoOther = request.CustomerSocialInfoOther,
                        AddtionalInfo = request.AddtionalInfo,
                        RelativeTel = request.RelativeTel   ,
                        ExpertiseInfoIsFirstTimeRent = request.ExpertiseInfoIsFirstTimeRent,
                        ExpertiseInfoTrustLevel = request.ExpertiseInfoTrustLevel,
                        CompanyInfo = request.CompanyInfo
                    };

                    // Save the new ContractGroupGenerallInfos object to the database
                    _contractContext.CustomerInfos.Add(customerInfo);
                    _contractContext.SaveChanges();
                    // Create new ContractGroup object and set its properties


                    int contractGroupIdDefault = Constant.ContractGroupConstant.ContractGroupIsExpertising;
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
                    //var ContractGroup = _contractContext.ContractGroups.Find(id);

                    //// Update the properties of the ContractGroup object
                    //ContractGroup.ParkingLotId = request.ParkingLotId;
                    //ContractGroup.ContractGroupStatus = request.ContractGroupStatus;
                    //ContractGroup.ContractGroupId = request.ContractGroupId;
                    //ContractGroup.ContractGroupLicensePlates = request.ContractGroupLicensePlates;
                    //ContractGroup.ContractGroupMake = request.ContractGroupMake;
                    //ContractGroup.SeatNumber = request.SeatNumber;
                    //ContractGroup.ContractGroupModel = request.ContractGroupModel;
                    //ContractGroup.ContractGroupGeneration = request.ContractGroupGeneration;
                    //ContractGroup.ContractGroupSeries = request.ContractGroupSeries;
                    //ContractGroup.ContractGroupTrim = request.ContractGroupTrim;
                    //ContractGroup.CreatedDate = request.CreatedDate;
                    //ContractGroup.IsDeleted = request.IsDeleted;
                    //ContractGroup.ContractGroupColor = request.ContractGroupColor;
                    //ContractGroup.ContractGroupFuel = request.ContractGroupFuel;

                    //// Save the changes to the database
                    //_contractContext.ContractGroups.Update(ContractGroup);
                    //_contractContext.SaveChanges();

                    //// Retrieve the ContractGroupGenerallInfos object from the database
                    //var ContractGroupGenerallInfos = _contractContext.ContractGroupGenerallInfos.Find(id);

                    //// Update the properties of the ContractGroupGenerallInfos object
                    //ContractGroupGenerallInfos.PriceForNormalDay = request.PriceForNormalDay;
                    //ContractGroupGenerallInfos.PriceForWeekendDay = request.PriceForWeekendDay;
                    //ContractGroupGenerallInfos.PriceForMonth = request.PriceForMonth;
                    //ContractGroupGenerallInfos.LimitedKmForMonth = request.LimitedKmForMonth;
                    //ContractGroupGenerallInfos.OverLimitedMileage = request.OverLimitedMileage;

                    //// Save the changes to the database
                    //_contractContext.ContractGroupGenerallInfos.Update(ContractGroupGenerallInfos);
                    //_contractContext.SaveChanges();

                    //// Retrieve the ContractGroupStates object from the database
                    //var ContractGroupStates = _contractContext.ContractGroupStates.Find(id);

                    //// Update the properties of the ContractGroupStates object
                    //ContractGroupStates.ContractGroupStatusDescription = request.ContractGroupStatusDescription;
                    //ContractGroupStates.CurrentEtcAmount = request.CurrentEtcAmount;
                    //ContractGroupStates.FuelPercent = request.FuelPercent;
                    //ContractGroupStates.SpeedometerNumber = request.SpeedometerNumber;

                    //// Save the changes to the database
                    //_contractContext.ContractGroupStates.Update(ContractGroupStates);
                    //_contractContext.SaveChanges();

                    //// Retrieve the ContractGroupLoanInfo object from the database
                    //var ContractGroupLoanInfo = _contractContext.ContractGroupLoanInfos.Find(id);
                    //ContractGroupLoanInfo.ContractGroupOwnerName = request.ContractGroupOwnerName;
                    //ContractGroupLoanInfo.RentalMethod = request.RentalMethod;
                    //ContractGroupLoanInfo.RentalDate = request.RentalDate;
                    //ContractGroupLoanInfo.SpeedometerNumberReceive = request.SpeedometerNumberReceive;
                    //if (request.PriceForDayReceive != null)
                    //{
                    //    ContractGroupLoanInfo.PriceForDayReceive = request.PriceForDayReceive;
                    //}
                    //else
                    //{
                    //    ContractGroupLoanInfo.PriceForDayReceive = null;
                    //}
                    //ContractGroupLoanInfo.PriceForMonthReceive = request.PriceForMonthReceive;
                    //ContractGroupLoanInfo.Insurance = request.Insurance;
                    //ContractGroupLoanInfo.Maintenance = request.Maintenance;
                    //ContractGroupLoanInfo.LimitedKmForMonthReceive = request.LimitedKmForMonthReceive;
                    //ContractGroupLoanInfo.OverLimitedMileageReceive = request.OverLimitedMileageReceive;
                    //_contractContext.ContractGroupLoanInfos.Update(ContractGroupLoanInfo);
                    //_contractContext.SaveChanges();

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

                public bool UpdateContractCarId(int id, int carId) 
                {
                    var ContractGroup = _contractContext.ContractGroups.Where(c => c.Id == id).FirstOrDefault();
                    if (ContractGroup == null)
                        return false;

                    ContractGroup.CarId = carId;
                    return Save();
                }
        
    }
        }
