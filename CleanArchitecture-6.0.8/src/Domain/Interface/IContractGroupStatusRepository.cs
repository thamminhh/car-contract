using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface IContractGroupStatusRepository
    {

        ContractGroupStatus GetContractGroupStatusById(int id);

        ICollection<ContractGroupStatus> GetContractGroupStatuss();

        bool ContractGroupStatusExit(int id);

        bool CreateContractGroupStatus(ContractGroupStatus ContractGroupStatus);
        bool UpdateContractGroupStatus(ContractGroupStatus ContractGroupStatus);
        bool Save();

        int GetContractGroupStatusIdByName(string name);    

    }
}
