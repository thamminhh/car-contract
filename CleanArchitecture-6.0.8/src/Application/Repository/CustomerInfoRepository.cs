using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CustomerInfoRepository : ICustomerInfoRepository
    {
        private readonly ContractContext _contractContext;

        public CustomerInfoRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CustomerInfo GetCustomerInfoById(int id)
        {
            return _contractContext.CustomerInfos.Where(c => c.Id == id).FirstOrDefault();
        }

        public CustomerInfo GetCustomerInfoByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<CustomerInfo> GetCustomerInfos()
        {
            return _contractContext.CustomerInfos.OrderBy(c => c.Id).ToList();
        }

        public bool CustomerInfoExit(int id)
        {
            return _contractContext.CustomerInfos.Any(c => c.Id == id);
        }

        public bool CreateCustomerInfo(CustomerInfo CustomerInfo)
        {
            _contractContext.Add(CustomerInfo);
            return Save();  
        }

        public bool UpdateCustomerInfo(CustomerInfo CustomerInfo)
        {
            _contractContext.Update(CustomerInfo);
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
