
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContractFile.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface IReceiveContractFileRepository
    {

        ICollection<ReceiveContractFileDataModel> GetReceiveContractFiles();

        ICollection<ReceiveContractFileDataModel> GetReceiveContractFilesByReceiveContractId(int receiveContractId);

        public ReceiveContractFileDataModel GetReceiveContractFileById(int ReceiveContractFileId);

        void CreateReceiveContractFile(ReceiveContractFileCreateModel request);

        //void UpdateReceiveContractFile(int id, ReceiveContractFileUpdateModel request);

    }
}
