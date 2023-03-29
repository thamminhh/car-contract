using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;

namespace CleanArchitecture.Domain.Interface
{
    public interface IRentContractRepository
    {

        RentContractDataModel GetRentContractById(int id);

        RentContractDataModel GetLastRentContractByContractGroupId(int contractGroupId);

        public ICollection<RentContractDataModel> GetRentContractByContractGroupId(int contractGroupId);

        void CreateRentContract(RentContractCreateModel request);

        void UpdateRentContract(int id, RentContractUpdateModel request);
        bool UpdateRentContractStatus(int id, RentContractUpdateStatusModel request);

        bool RentContractExit(int id);  

    }
}
