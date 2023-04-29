
using CleanArchitecture.Domain.Entities_SubModel.RentContractFile.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface IRentContractFileRepository
    {

        Task CreateRentContractFiles(List<RentContractFileCreateModel> rentContractFiles);

        ICollection<RentContractFileDataModel> GetRentContractFilesByRentContractId(int rentContractId);

        Task UpdateRentContractFiles(List<RentContractFileUpdateModel> rentContractFiles);

        //public RentContractFileDataModel GetRentContractFileById(int rentContractFileId);

        //void CreateRentContractFile(RentContractFileCreateModel request);
        Task<bool> DeleteRentContractFile(int rentContractFileId);

        //void UpdateRentContractFile(int id, RentContractFileUpdateModel request);

    }
}
