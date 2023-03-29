
using CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface ITransferContractFileRepository
    {

        ICollection<TransferContractFileDataModel> GetTransferContractFiles();

        ICollection<TransferContractFileDataModel> GetTransferContractFilesByTransferContractId(int transferContractId);

        public TransferContractFileDataModel GetTransferContractFileById(int TransferContractFileId);

        void CreateTransferContractFile(TransferContractFileCreateModel request);

        //void UpdateTransferContractFile(int id, TransferContractFileUpdateModel request);

    }
}
