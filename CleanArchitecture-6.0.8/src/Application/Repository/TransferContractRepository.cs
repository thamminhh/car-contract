using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.TransferContract;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class TransferContractRepository : ITransferContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupRepository;

        public TransferContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupRepository)
        {
            _contractContext = contractContext;
            _contractGroupRepository = contractGroupRepository;
        }

        public TransferContract GetTransferContractById(int id)
        {
            return _contractContext.TransferContracts.Where(c => c.Id == id).FirstOrDefault();
        }

        public TransferContract GetTransferContractByContractGroupId(int contractGroupId)
        {
            return _contractContext.TransferContracts.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();
        }


        public bool TransferContractExit(int id)
        {
            return _contractContext.TransferContracts.Any(c => c.Id == id);
        }

        public void CreateExpertiesContract(TransferContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExporting;

            var transferContract = new TransferContract
            {
                ContractGroupId = request.ContractGroupId,
                TransfererId = request.TransfererId,
                DateTransfer = request.DateTransfer,
                DeliveryAddress = request.DeliveryAddress,
                CurrentCarStateSpeedometerNumber = request.CurrentCarStateSpeedometerNumber,
                CurrentCarStateFuelPercent = request.CurrentCarStateFuelPercent,
                CurrentCarStateCurrentEtcAmount = request.CurrentCarStateCurrentEtcAmount,
                CurrentCarStateCarStatusDescription = request.CurrentCarStateCarStatusDescription,
                CurrentCarStateCarFrontImg = request.CurrentCarStateCarFrontImg,
                CurrentCarStateCarBackImg = request.CurrentCarStateCarBackImg,
                CurrentCarStateCarLeftImg = request.CurrentCarStateCarLeftImg,
                CurrentCarStateCarRightImg = request.CurrentCarStateCarRightImg,
                CurrentCarStateCarInteriorImg = request.CurrentCarStateCarInteriorImg,
                CurrentCarStateCarBackSeatImg = request.CurrentCarStateCarBackSeatImg,
                DepositItemPaper = request.DepositItemPaper,
                DepositItemAsset = request.DepositItemAsset,
                DepositItemAssetInfo = request.DepositItemAssetInfo,
                CreatedDate = request.CreatedDate,
                ContractStatusId = defaultContractId
            };
            _contractContext.TransferContracts.Add(transferContract);
            _contractContext.SaveChanges();

            var contractGroupStatusExpertised = Constant.ContractGroupConstant.TransferContractNotSign;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusExpertised;

            _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
        }

        public void UpdateTransferContract(int id, TransferContractUpdateModel request)
        {
            var transferContract = _contractContext.TransferContracts.Find(id);
            transferContract.ContractGroupId = request.ContractGroupId;
            transferContract.TransfererId = request.TransfererId;
            transferContract.DateTransfer = request.DateTransfer;
            transferContract.DeliveryAddress = request.DeliveryAddress;
            transferContract.CurrentCarStateSpeedometerNumber = request.CurrentCarStateSpeedometerNumber;
            transferContract.CurrentCarStateFuelPercent = request.CurrentCarStateFuelPercent;
            transferContract.CurrentCarStateCurrentEtcAmount = request.CurrentCarStateCurrentEtcAmount;
            transferContract.CurrentCarStateCarStatusDescription = request.CurrentCarStateCarStatusDescription;
            transferContract.CurrentCarStateCarFrontImg = request.CurrentCarStateCarFrontImg;
            transferContract.CurrentCarStateCarBackImg = request.CurrentCarStateCarBackImg;
            transferContract.CurrentCarStateCarLeftImg = request.CurrentCarStateCarBackImg;
            transferContract.CurrentCarStateCarRightImg = request.CurrentCarStateCarRightImg;
            transferContract.CurrentCarStateCarInteriorImg = request.CurrentCarStateCarInteriorImg;
            transferContract.CurrentCarStateCarBackSeatImg = request.CurrentCarStateCarBackSeatImg;
            transferContract.DepositItemPaper = request.DepositItemPaper;
            transferContract.DepositItemAsset = request.DepositItemAsset;
            transferContract.DepositItemAssetInfo = request.DepositItemAssetInfo;

            transferContract.IsExported = request.IsExported;
            transferContract.CustomerSignature = request.CustomerSignature;
            transferContract.StaffSignature = request.StaffSignature;
            transferContract.ContractStatusId = request.ContractStatusId;

            _contractContext.TransferContracts.Update(transferContract);
            _contractContext.SaveChanges();
        }

        public bool UpdateTransferContractStatus(int id, TransferContractUpdateStatusModel request)
        {
            var transferContract = _contractContext.TransferContracts.Where(c => c.Id == id).FirstOrDefault();

            if (transferContract == null)
                return false;

            transferContract.ContractStatusId = request.ContractStatusId;
            return Save();
        }
        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
