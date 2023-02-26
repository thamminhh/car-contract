using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.TransferContract;

namespace CleanArchitecture.Domain.Interface
{
    public interface ITransferContractRepository
    {

        TransferContract GetTransferContractById(int id);

        TransferContract GetTransferContractByContractGroupId(int contractGroupId);

        void CreateExpertiesContract(TransferContractCreateModel request);

        void UpdateTransferContract(int id, TransferContractUpdateModel request);
        bool UpdateTransferContractStatus(int id, TransferContractUpdateStatusModel request);

        bool TransferContractExit(int id);  

    }
}
