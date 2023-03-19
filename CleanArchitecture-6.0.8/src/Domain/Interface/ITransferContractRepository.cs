using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.TransferContract;

namespace CleanArchitecture.Domain.Interface
{
    public interface ITransferContractRepository
    {

        TransferContractDataModel GetTransferContractById(int id);

        TransferContractDataModel GetTransferContractByContractGroupId(int contractGroupId);

        void CreateTransferContract(TransferContractCreateModel request);

        void UpdateTransferContract(int id, TransferContractUpdateModel request);
        bool UpdateTransferContractStatus(int id, TransferContractUpdateStatusModel request);

        bool TransferContractExit(int id);  

    }
}
