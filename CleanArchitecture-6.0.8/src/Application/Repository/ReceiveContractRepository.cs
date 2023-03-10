using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class ReceiveContractRepository : IReceiveContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupRepository;

        public ReceiveContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupRepository)
        {
            _contractContext = contractContext;
            _contractGroupRepository = contractGroupRepository;
        }

        public ReceiveContract GetReceiveContractById(int id)
        {
            return _contractContext.ReceiveContracts.Where(c => c.Id == id).FirstOrDefault();
        }

        public ReceiveContract GetReceiveContractByContractGroupId(int contractGroupId)
        {
            return _contractContext.ReceiveContracts.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();
        }


        public bool ReceiveContractExit(int id)
        {
            return _contractContext.ReceiveContracts.Any(c => c.Id == id);
        }

        public void CreateExpertiesContract(ReceiveContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExporting;

            var receiveContract = new ReceiveContract
            {
                ContractGroupId = request.ContractGroupId,
                ReceiverId = request.ReceiverId,
                DateReceive = request.DateReceive,
                ReceiveAddress = request.ReceiveAddress,
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
                CurrentCarStateCarPhysicalDamage = request.CurrentCarStateCarPhysicalDamage,
                DepositItemAsset = request.DepositItemAsset,
                CarInsuranceMoney = request.CarInsuranceMoney,
                CreatedDate = request.CreatedDate,
                ContractStatusId = defaultContractId
            };
            _contractContext.ReceiveContracts.Add(receiveContract);
            _contractContext.SaveChanges();

            var contractGroupStatusExpertised = Constant.ContractGroupConstant.ReceiveContractNotSign;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusExpertised;

            _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
        }

        public void UpdateReceiveContract(int id, ReceiveContractUpdateModel request)
        {
            var receiveContract = _contractContext.ReceiveContracts.Find(id);
            receiveContract.ContractGroupId = request.ContractGroupId;
            receiveContract.ReceiverId = request.ReceiverId;
            receiveContract.DateReceive = request.DateReceive;
            receiveContract.ReceiveAddress = request.ReceiveAddress;
            receiveContract.CurrentCarStateSpeedometerNumber = request.CurrentCarStateSpeedometerNumber;
            receiveContract.CurrentCarStateFuelPercent = request.CurrentCarStateFuelPercent;
            receiveContract.CurrentCarStateCurrentEtcAmount = request.CurrentCarStateCurrentEtcAmount;
            receiveContract.CurrentCarStateCarStatusDescription = request.CurrentCarStateCarStatusDescription;
            receiveContract.CurrentCarStateCarFrontImg = request.CurrentCarStateCarFrontImg;
            receiveContract.CurrentCarStateCarBackImg = request.CurrentCarStateCarBackImg;
            receiveContract.CurrentCarStateCarLeftImg = request.CurrentCarStateCarBackImg;
            receiveContract.CurrentCarStateCarRightImg = request.CurrentCarStateCarRightImg;
            receiveContract.CurrentCarStateCarInteriorImg = request.CurrentCarStateCarInteriorImg;
            receiveContract.CurrentCarStateCarBackSeatImg = request.CurrentCarStateCarBackSeatImg;
            receiveContract.CurrentCarStateCarPhysicalDamage = request.CurrentCarStateCarPhysicalDamage;
            receiveContract.DepositItemAsset = request.DepositItemAsset;
            receiveContract.CarInsuranceMoney = request.CarInsuranceMoney;
            receiveContract.IsExported = request.IsExported;
            receiveContract.CustomerSignature = request.CustomerSignature;
            receiveContract.StaffSignature = request.StaffSignature;
            receiveContract.ContractStatusId = request.ContractStatusId;

            _contractContext.ReceiveContracts.Update(receiveContract);
            _contractContext.SaveChanges();
        }

        public bool UpdateReceiveContractStatus(int id, ReceiveContractUpdateStatusModel request)
        {
            var receiveContract = _contractContext.ReceiveContracts.Where(c => c.Id == id).FirstOrDefault();

            if (receiveContract == null)
                return false;

            receiveContract.ContractStatusId = request.ContractStatusId;
            return Save();
        }
        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
