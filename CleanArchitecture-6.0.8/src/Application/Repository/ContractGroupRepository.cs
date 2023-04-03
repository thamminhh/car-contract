
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class ContractGroupRepository : IContractGroupRepository
    {
        private readonly ContractContext _contractContext;
        private readonly ICustomerFileRepository _customerFileRepository;


        public ContractGroupRepository(ContractContext contractContext, ICustomerFileRepository customerFile)
        {
            _contractContext = contractContext;
            _customerFileRepository = customerFile;
        }

        //private static string changeIdContractIntoString(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
        //    if (id == 1)
        //    {
        //        return ContractStatusConstant.ContractExportingName;
        //    }
        //    if (id == 2)
        //    {
        //        return ContractStatusConstant.ContractExportedName;
        //    }
        //    if (id == 3)
        //    {
        //        return ContractStatusConstant.ContractCancelledName;
        //    }
        //    return "";

        //}

        public int GetNumberOfContracts(ContractFilter filter)
        {
            {
                IQueryable<ContractGroup> contractGroups = _contractContext.ContractGroups;

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
                    if (!string.IsNullOrWhiteSpace(filter.CitizenIdentificationInfoNumber))
                    {
                        contractGroups = contractGroups.Where(c => c.CustomerInfo.CitizenIdentificationInfoNumber == filter.CitizenIdentificationInfoNumber);
                    }

                }
                return contractGroups.Count();
            }
        }

        public int GetContractGroupsByParkingLotId(int parkingLotId, ContractFilter filter)
        {
            {
                IQueryable<ContractGroup> contractGroups = _contractContext.ContractGroups
                    .Include(c => c.CustomerInfo)
                    .Include(c => c.User)
                    .Include(c => c.ContractGroupStatus)
                    .Where(c => c.Car.ParkingLotId == parkingLotId)
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
                    if (!string.IsNullOrWhiteSpace(filter.CitizenIdentificationInfoNumber))
                    {
                        contractGroups = contractGroups.Where(c => c.CustomerInfo.CitizenIdentificationInfoNumber == filter.CitizenIdentificationInfoNumber);
                    }

                }
                return contractGroups.Count();
            }
        }
        public ContractGroupDataModel GetContractGroupById(int contractGroupId)
        {
            ContractGroup contractGroup = _contractContext.ContractGroups
                .FirstOrDefault(c => c.Id == contractGroupId);

            var customerInfo = _contractContext.CustomerInfos.Find(contractGroup.CustomerInfoId);
            var contractGroupStatus = _contractContext.ContractGroupStatuses.Find(contractGroup.ContractGroupStatusId);
            var customerFiles = _customerFileRepository.GetCustomerFilesByCustomerInfoId(customerInfo.Id);

            return new ContractGroupDataModel
            {
                Id = contractGroup.Id,
                CustomerInfoId = contractGroup.CustomerInfoId,
                UserId = contractGroup.UserId,
                CarId = contractGroup.CarId,
                RentPurpose = contractGroup.RentPurpose,
                RentFrom = contractGroup.RentFrom,
                RentTo = contractGroup.RentTo,
                RequireDescriptionInfoCarBrand = contractGroup.RequireDescriptionInfoCarBrand,
                RequireDescriptionInfoSeatNumber = contractGroup.RequireDescriptionInfoSeatNumber,
                RequireDescriptionInfoPriceForDay = contractGroup.RequireDescriptionInfoPriceForDay,
                RequireDescriptionInfoCarColor = contractGroup.RequireDescriptionInfoCarColor,
                RequireDescriptionInfoGearBox = contractGroup.RequireDescriptionInfoGearBox,
                DeliveryAddress = contractGroup.DeliveryAddress,

                ContractGroupStatusId = contractGroup.ContractGroupStatusId,
                ContractGroupStatusName = contractGroupStatus.Name,

                CustomerName = customerInfo.CustomerName,
                PhoneNumber = customerInfo.PhoneNumber,
                CustomerAddress = customerInfo.CustomerAddress,
                CitizenIdentificationInfoNumber = customerInfo.CitizenIdentificationInfoNumber,
                CitizenIdentificationInfoAddress = customerInfo.CitizenIdentificationInfoAddress,
                CitizenIdentificationInfoDateReceive = customerInfo.CitizenIdentificationInfoDateReceive,
                CustomerSocialInfoZalo = customerInfo.CustomerSocialInfoZalo,
                CustomerSocialInfoFacebook = customerInfo.CustomerSocialInfoFacebook,
                RelativeTel = customerInfo.RelativeTel,
                CompanyInfo = customerInfo.CompanyInfo,
                CustomerEmail = customerInfo.CustomerEmail,

                CustomerFiles = customerFiles,

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
                if (!string.IsNullOrWhiteSpace(filter.CitizenIdentificationInfoNumber))
                {
                    contractGroups = contractGroups.Where(c => c.CustomerInfo.CitizenIdentificationInfoNumber == filter.CitizenIdentificationInfoNumber);
                }

            }

            var contractGroupDataModels = contractGroups
                .OrderByDescending(c => c.Id)
                .Skip(skip)
                .Take(pageSize)
                .Select(c => new ContractGroupDataModel
                {
                    Id = c.Id,
                    CustomerName = c.CustomerInfo.CustomerName,
                    UserId = c.UserId,
                    StaffEmail = c.User.Email,
                    ContractGroupStatusId = c.ContractGroupStatusId,
                    ContractGroupStatusName = c.ContractGroupStatus.Name,
                    CitizenIdentificationInfoNumber = c.CustomerInfo.CitizenIdentificationInfoNumber,
                })
                .ToList();

            return contractGroupDataModels;
        }

        public ICollection<ContractGroupDataModel> GetContractGroupsByParkingLotId(int page, int pageSize, int parkingLotId, ContractFilter filter)

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
                .Where(c => c.Car.ParkingLotId == parkingLotId)
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
                if (!string.IsNullOrWhiteSpace(filter.CitizenIdentificationInfoNumber))
                {
                    contractGroups = contractGroups.Where(c => c.CustomerInfo.CitizenIdentificationInfoNumber == filter.CitizenIdentificationInfoNumber);
                }
            }

            var contractGroupDataModels = contractGroups
                .OrderByDescending(c => c.Id)
                .Skip(skip)
                .Take(pageSize)
                .Select(c => new ContractGroupDataModel
                {
                    Id = c.Id,
                    CustomerName = c.CustomerInfo.CustomerName,
                    UserId = c.UserId,
                    StaffEmail = c.User.Email,
                    ContractGroupStatusId = c.ContractGroupStatusId,
                    ContractGroupStatusName = c.ContractGroupStatus.Name,
                    CitizenIdentificationInfoNumber = c.CustomerInfo.CitizenIdentificationInfoNumber,

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
                PhoneNumber = request.CustomerPhoneNumber,
                CustomerName = request.CustomerName,
                CustomerAddress = request.CustomerAddress,
                CustomerEmail = request.CustomerEmail,
                CitizenIdentificationInfoNumber = request.CustomerCitizenIdentificationInfoNumber,
                CitizenIdentificationInfoAddress = request.CustomerCitizenIdentificationInfoAddress,
                CitizenIdentificationInfoDateReceive = request.CustomerCitizenIdentificationInfoDate,
                CustomerSocialInfoZalo = request.CustomerSocialInfoZalo,
                CustomerSocialInfoFacebook = request.CustomerSocialInfoFacebook,
                RelativeTel = request.RelativeTel,
                CompanyInfo = request.CompanyInfo,

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
                RequireDescriptionInfoPriceForDay = request.RequireDescriptionInfoPriceForDay,
                RequireDescriptionInfoCarColor = request.RequireDescriptionInfoCarColor,
                RequireDescriptionInfoGearBox = request.RequireDescriptionInfoGearBox,
                DeliveryAddress = request.CustomerAddress,
                ContractGroupStatusId = contractGroupIdDefault,
            };

            // Save the new ContractGroup to the database
            _contractContext.ContractGroups.Add(contractGroup);
            _contractContext.SaveChanges();

            var customerFiles = request.CustomerFiles;
            List<CustomerFile> customerFileList = new List<CustomerFile>();

            if (customerFiles != null && customerFiles.Any())
            {
                foreach (var fileCreate in customerFiles)
                {
                    var file = new CustomerFile
                    {
                        CustomerInfoId = customerInfo.Id,
                        TypeOfDocument = fileCreate.TypeOfDocument,
                        Title = fileCreate.Title,
                        DocumentImg = fileCreate.DocumentImg,
                        DocumentDescription = fileCreate.DocumentDescription,
                    };

                    customerFileList.Add(file);
                }

                // add all new CustomerFiles to the database context in a single transaction
                _contractContext.CustomerFiles.AddRange(customerFileList);
                _contractContext.SaveChanges();
            }
        }

        public void UpdateContractGroup(int id, ContractGroupUpdateModel request)
        {
            // Retrieve the ContractGroup object from the database
            var contractGroup = _contractContext.ContractGroups.Find(id);

            // Update the properties of the ContractGroup object
            contractGroup.CustomerInfoId = request.CustomerInfoId;
            contractGroup.UserId = request.UserId;
            contractGroup.CarId = request.CarId;
            contractGroup.RentPurpose = request.RentPurpose;
            contractGroup.RentFrom = request.RentFrom;
            contractGroup.RentTo = request.RentTo;
            contractGroup.RequireDescriptionInfoCarBrand = request.RequireDescriptionInfoCarBrand;
            contractGroup.RequireDescriptionInfoSeatNumber = request.RequireDescriptionInfoSeatNumber;
            contractGroup.RequireDescriptionInfoPriceForDay = request.RequireDescriptionInfoPriceForDay;
            contractGroup.RequireDescriptionInfoGearBox = request.RequireDescriptionInfoGearBox;
            contractGroup.RequireDescriptionInfoCarColor = request.RequireDescriptionInfoCarColor;
            contractGroup.DeliveryAddress = request.DeliveryAddress;
            contractGroup.ContractGroupStatusId = request.ContractGroupStatusId;


            // Save the changes to the database
            _contractContext.ContractGroups.Update(contractGroup);
            _contractContext.SaveChanges();

            // Retrieve the ContractGroupGenerallInfos object from the database
            var customerInfo = _contractContext.CustomerInfos.Find(request.CustomerInfoId);
            var customerFiles = request.CustomerFiles;
            if (customerInfo != null)
            {
                customerInfo.CustomerName = request.CustomerName;
                customerInfo.PhoneNumber = request.PhoneNumber;
                customerInfo.CustomerAddress = request.CustomerAddress;
                customerInfo.CitizenIdentificationInfoAddress = request.CitizenIdentificationInfoAddress;
                customerInfo.CitizenIdentificationInfoDateReceive = request.CitizenIdentificationInfoDateReceive;
                customerInfo.CustomerSocialInfoZalo = request.CustomerSocialInfoZalo;
                customerInfo.CustomerSocialInfoFacebook = request.CustomerSocialInfoFacebook;
                customerInfo.RelativeTel = request.RelativeTel;
                customerInfo.CompanyInfo = request.CompanyInfo;
                customerInfo.CustomerEmail = request.CustomerEmail;

                if (customerFiles != null)
                {
                    foreach (var file in customerFiles)
                    {
                        var existingFile = _contractContext.CustomerFiles.FirstOrDefault(cf => cf.Id == file.Id && cf.CustomerInfoId == customerInfo.Id);
                        if (existingFile != null)
                        {
                            existingFile.TypeOfDocument = file.TypeOfDocument ?? existingFile.TypeOfDocument;
                            existingFile.Title = file.Title ?? existingFile.Title;
                            existingFile.DocumentImg = file.DocumentImg ?? existingFile.DocumentImg;
                            existingFile.DocumentDescription = file.DocumentDescription ?? existingFile.DocumentDescription;
                        }
                        else
                        {
                            _contractContext.CustomerFiles.Add(new CustomerFile
                            {
                                CustomerInfoId = customerInfo.Id,
                                TypeOfDocument = file.TypeOfDocument,
                                Title = file.Title,
                                DocumentImg = file.DocumentImg,
                                DocumentDescription = file.DocumentDescription,
                            });
                        }

                    }
                }
                _contractContext.CustomerInfos.Update(customerInfo);
                _contractContext.SaveChanges();
            }
        }

        // Retrieve the ContractGroupStates object from the database

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
