using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;

namespace CleanArchitecture.Domain.Interface
{
    public interface IReceiveContractRepository
    {

        ReceiveContract GetReceiveContractById(int id);

        ReceiveContract GetReceiveContractByContractGroupId(int contractGroupId);

        void CreateExpertiesContract(ReceiveContractCreateModel request);

        void UpdateReceiveContract(int id, ReceiveContractUpdateModel request);
        bool UpdateReceiveContractStatus(int id, ReceiveContractUpdateStatusModel request);

        bool ReceiveContractExit(int id);  

    }
}
