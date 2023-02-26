using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class ContractFileRepository : IContractFileRepository
    {
        private readonly ContractContext _contractContext;

        public ContractFileRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public ContractFile GetContractFileById(int id)
        {
            return _contractContext.ContractFiles.Where(c => c.Id == id).FirstOrDefault();
        }

        public ContractFile GetContractFileByContractGroupId(int contractGroupId)
        {
            return _contractContext.ContractFiles.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();
        }


        public bool ContractFileExit(int id)
        {
            return _contractContext.ContractFiles.Any(c => c.Id == id);
        }
            
    }
}
