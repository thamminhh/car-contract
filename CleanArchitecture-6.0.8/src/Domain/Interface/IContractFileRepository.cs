using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface IContractFileRepository
    {

        ContractFile GetContractFileById(int id);

        ContractFile GetContractFileByContractGroupId(int contractGroupId);

        bool ContractFileExit(int id);  

    }
}
