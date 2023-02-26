using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class ContractGroupStatusRepository : IContractGroupStatusRepository
    {
        private readonly ContractContext _contractContext;

        public ContractGroupStatusRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        int IContractGroupStatusRepository.GetContractGroupStatusIdByName(string name)
        {
            ContractGroupStatus ContractGroupStatus = _contractContext.ContractGroupStatuses.Where(c => c.Name == name).FirstOrDefault();
            return ContractGroupStatus.Id;
        }
        public ContractGroupStatus GetContractGroupStatusById(int id)
        {
            return _contractContext.ContractGroupStatuses.Where(c => c.Id == id).FirstOrDefault();
        }

        public ContractGroupStatus GetContractGroupStatusByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<ContractGroupStatus> GetContractGroupStatuss()
        {
            return _contractContext.ContractGroupStatuses.OrderBy(c => c.Id).ToList();
        }

        public bool ContractGroupStatusExit(int id)
        {
            return _contractContext.ContractGroupStatuses.Any(c => c.Id == id);
        }

        public bool CreateContractGroupStatus(ContractGroupStatus ContractGroupStatus)
        {
            _contractContext.Add(ContractGroupStatus);
            return Save();  
        }

        public bool UpdateContractGroupStatus(ContractGroupStatus ContractGroupStatus)
        {
            _contractContext.Update(ContractGroupStatus);
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
