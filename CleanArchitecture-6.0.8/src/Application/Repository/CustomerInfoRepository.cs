using System.Globalization;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CustomerFiles.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CustomerInfos;
using CleanArchitecture.Domain.Interface;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Repository
{
    public class CustomerInfoRepository : ICustomerInfoRepository
    {
        private readonly ContractContext _contractContext;
        private readonly ICustomerFileRepository _customerFileRepository;

        public CustomerInfoRepository(ContractContext contractContext, ICustomerFileRepository customerFileRepository)
        {
            _contractContext = contractContext;
            _customerFileRepository = customerFileRepository;
        }

        public CustomerInfo GetCustomerInfoById(int id)
        {
            return _contractContext.CustomerInfos.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<CustomerInfo> GetCustomerInfos()
        {
            return _contractContext.CustomerInfos.OrderBy(c => c.Id).ToList();
        }

        public bool CustomerInfoExit(string citizenIdentificationInfoNumber)
        {
            return _contractContext.CustomerInfos.Any(c => c.CitizenIdentificationInfoNumber == citizenIdentificationInfoNumber);
        }

        public bool CreateCustomerInfo(CustomerInfo customerInfo)
        {
            _contractContext.Add(customerInfo);
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public CustomerInfoDataModel GetCustomerInfoByCitizenIdentificationInfoNumber(string citizenIdentificationInfoNumber)
        {
            var customerInfo = _contractContext.CustomerInfos
                .Where(c => c.CitizenIdentificationInfoNumber == citizenIdentificationInfoNumber).FirstOrDefault();

            var customerFiles = _customerFileRepository.GetCustomerFilesByCustomerInfoId(customerInfo.Id);
            return new CustomerInfoDataModel
            {
                Id = customerInfo.Id,
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

        public bool UpdateCustomerInfo(string citizenIdentificationInfoNumber, CustomerInfoUpdateModel request)
        {
            var customerInfo = _contractContext.CustomerInfos
                .Where(c => c.CitizenIdentificationInfoNumber == citizenIdentificationInfoNumber).FirstOrDefault();
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
                return true;
            }
            return false;
        }

    }
}
