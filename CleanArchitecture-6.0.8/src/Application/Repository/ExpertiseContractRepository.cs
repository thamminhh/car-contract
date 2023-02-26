using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ExpertiseContract;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class ExpertiseContractRepository : IExpertiseContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupController;

        public ExpertiseContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupController)
        {
            _contractContext = contractContext;
            _contractGroupController = contractGroupController;
        }

        public ExpertiseContract GetExpertiseContractById(int id)
        {
            return _contractContext.ExpertiseContracts.Where(c => c.Id == id).FirstOrDefault();
        }

        public ExpertiseContract GetExpertiseContractByContractGroupId(int contractGroupId)
        {
            return _contractContext.ExpertiseContracts.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();
        }


        public bool ExpertiseContractExit(int id)
        {
            return _contractContext.ExpertiseContracts.Any(c => c.Id == id);
        }

        public void CreateExpertiesContract(ExpertiseContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExported;

            _contractGroupController.UpdateContractCarId(request.ContractGroupId, request.CarId);

            var expertiseContract = new ExpertiseContract
            {
                ContractGroupId = request.ContractGroupId,
                ExpertiserId = request.ExpertiserId,
                ExpertiseDate = request.ExpertiseDate,
                Description = request.Description,
                Result = request.Result,
                ResultOther = request.ResultOther,
                TrustLevel = request.TrustLevel,
                DepositInfoDescription = request.DepositInfoDescription,
                DepositInfoAsset = request.DepositInfoAsset,
                DepositInfoDownPayment = request.DepositInfoDownPayment,
                PaymentAmount = request.PaymentAmount,
                ContractStatusId = defaultContractId
            };
            _contractContext.ExpertiseContracts.Add(expertiseContract);
            _contractContext.SaveChanges();
            
        }

        public void UpdateExpertiseContract(int id, ExpertiseContractUpdateModel request)
        {
            var expertiseContract = _contractContext.ExpertiseContracts.Find(id);
            expertiseContract.ContractGroupId = request.ContractGroupId;
            expertiseContract.ExpertiserId = request.ExpertiserId;
            expertiseContract.ExpertiseDate = request.ExpertiseDate;
            expertiseContract.Description = request.Description;
            expertiseContract.Result = request.Result;
            expertiseContract.ResultOther = request.ResultOther;
            expertiseContract.TrustLevel = request.TrustLevel;
            expertiseContract.DepositInfoDescription = request.DepositInfoDescription;
            expertiseContract.DepositInfoAsset = request.DepositInfoAsset;
            expertiseContract.DepositInfoDownPayment = request.DepositInfoDownPayment;
            expertiseContract.ContractStatusId = request.ContractStatusId;

            _contractContext.ExpertiseContracts.Update(expertiseContract);
            _contractContext.SaveChanges();

        }

        public bool UpdateExpertiseContractStatus(int id, ExpertiseContractUpdateStatusModel request)
        {
            var expertiseContract = _contractContext.ExpertiseContracts.Where(c => c.Id == id).FirstOrDefault();

            if (expertiseContract == null)
                return false;

            expertiseContract.ContractStatusId = request.ContractStatusId;
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
