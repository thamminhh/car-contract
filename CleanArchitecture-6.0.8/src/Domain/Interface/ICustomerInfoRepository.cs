using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICustomerInfoRepository
    {

        CustomerInfo GetCustomerInfoById(int id);

        ICollection<CustomerInfo> GetCustomerInfos();

        bool CustomerInfoExit(int id);

        bool CreateCustomerInfo(CustomerInfo CustomerInfo);
        bool UpdateCustomerInfo(CustomerInfo CustomerInfo);
        bool Save();

    }
}
