using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ExpertiseContract;
using CleanArchitecture.Domain.Interface;
using MediatR;

namespace CleanArchitecture.Application.Repository
{
    public class ExpertiseContractRepository : IExpertiseContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupController;
        private readonly FileRepository _fileRepository;

        public ExpertiseContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupController, FileRepository fileRepository)
        {
            _contractContext = contractContext;
            _contractGroupController = contractGroupController;
            _fileRepository = fileRepository;
        }

        public ExpertiseContractDataModel GetExpertiseContractById(int id)
        {
            var expertisecontracts = _contractContext.ExpertiseContracts.Where(c => c.Id == id).FirstOrDefault();
            var contractGroup = _contractContext.ContractGroups.Find(expertisecontracts.ContractGroupId);
            var host = _fileRepository.GetCurrentHost();
            return new ExpertiseContractDataModel
            {
                Id= id,
                ContractGroupId = expertisecontracts.ContractGroupId,
                ExpertiserId = expertisecontracts.ExpertiserId,
                CarId = contractGroup.CarId,
                ExpertiseDate = expertisecontracts.ExpertiseDate,
                Description = expertisecontracts.Description,
                Result = expertisecontracts.Result,
                ResultOther = expertisecontracts.ResultOther,
                TrustLevel = expertisecontracts.TrustLevel,
                DepositInfoDescription = expertisecontracts.DepositInfoDescription,
                DepositInfoAsset = expertisecontracts.DepositInfoAsset,
                FilePath = host + expertisecontracts.FilePath,
                DepositInfoDownPayment = expertisecontracts.DepositInfoDownPayment,
                PaymentAmount = expertisecontracts.PaymentAmount,
                ContractStatusId = expertisecontracts.ContractStatusId
            };
        }

        public ExpertiseContractDataModel GetExpertiseContractByContractGroupId(int contractGroupId)
        {
            var expertisecontracts = _contractContext.ExpertiseContracts.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();

            var contractGroup = _contractContext.ContractGroups.Find(expertisecontracts.ContractGroupId);
            var host = _fileRepository.GetCurrentHost();
            return new ExpertiseContractDataModel
            {
                Id = expertisecontracts.Id,
                ContractGroupId = expertisecontracts.ContractGroupId,
                ExpertiserId = expertisecontracts.ExpertiserId,
                CarId = contractGroup.CarId,
                ExpertiseDate = expertisecontracts.ExpertiseDate,
                Description = expertisecontracts.Description,
                Result = expertisecontracts.Result,
                ResultOther = expertisecontracts.ResultOther,
                TrustLevel = expertisecontracts.TrustLevel,
                DepositInfoDescription = expertisecontracts.DepositInfoDescription,
                DepositInfoAsset = expertisecontracts.DepositInfoAsset,
                FilePath = host + expertisecontracts.FilePath,
                DepositInfoDownPayment = expertisecontracts.DepositInfoDownPayment,
                PaymentAmount = expertisecontracts.PaymentAmount,
                ContractStatusId = expertisecontracts.ContractStatusId
            };
        }


        public bool ExpertiseContractExit(int id)
        {
            return _contractContext.ExpertiseContracts.Any(c => c.Id == id);
        }

        public void CreateExpertiesContract(ExpertiseContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExported;

            _contractGroupController.UpdateContractCarId(request.ContractGroupId, request.CarId);

            string htmlContent = "<h1> Hợp đồng thẩm định </h1>";
            string fileName = "ExpertiseContract" + ".pdf";

            htmlContent += "<h2> ExpertiserId: " + request.ExpertiserId + "</h2>";
            htmlContent += "<h2> ContractGroupId: " + request.ContractGroupId + "</h2>";
            htmlContent += "<h2> ExpertiseDate: " + request.ExpertiseDate + "</h2>";
            htmlContent += "<h2> Description: " + request.Description + "</h2>";
            htmlContent += "<h2> Result: " + request.Result + "<h2>";
            htmlContent += "<h2> DepositInfoDescription: " + request.DepositInfoDescription + "</h2>";
            htmlContent += "<h2> DepositInfoAsset: " + request.DepositInfoAsset + "</h2>";
            htmlContent += "<h2> DepositInfoDownPayment: " + request.DepositInfoDownPayment + "</h2>";
            htmlContent += "<h2> PaymentAmount: " + request.PaymentAmount + "</h2>";

            var file =  _fileRepository.GeneratePdfAsync(htmlContent, fileName);

            var filePath = _fileRepository.SaveFileToFolder(file, "1");

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
                FilePath = filePath,
                DepositInfoDownPayment = request.DepositInfoDownPayment,
                PaymentAmount = request.PaymentAmount,
                ContractStatusId = defaultContractId
            };
            _contractContext.ExpertiseContracts.Add(expertiseContract);
            _contractContext.SaveChanges();

            var contractGroupStatusExpertised = Constant.ContractGroupConstant.ContractGroupIsExpertising;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusExpertised;

            _contractGroupController.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
            
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
