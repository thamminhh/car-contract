
using CleanArchitecture.Domain.Entities_SubModel.CustomerFiles.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICustomerFileRepository
    {

        ICollection<CustomerFileDataModel> GetCustomerFiles();

        ICollection<CustomerFileDataModel> GetCustomerFilesByCustomerInfoId(int customerInfoId);

        public CustomerFileDataModel GetCustomerFileById(int customerFileId);

        void CreateCustomerFile(CustomerFileCreateModel request);

        //void UpdateCustomerFile(int id, CustomerFileUpdateModel request);

    }
}
