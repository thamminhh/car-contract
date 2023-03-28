using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CustomerInfos;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICustomerInfoRepository
    {

        CustomerInfo GetCustomerInfoById(int id);
        CustomerInfoDataModel GetCustomerInfoByCitizenIdentificationInfoNumber(string citizenIdentificationInfoNumber);

        ICollection<CustomerInfo> GetCustomerInfos();

        public bool CustomerInfoExit(string citizenIdentificationInfoNumber);

        bool CreateCustomerInfo(CustomerInfo customerInfo);
        public bool UpdateCustomerInfo(string citizenIdentificationInfoNumber, CustomerInfoUpdateModel customerInfo);
        bool Save();

    }
}
