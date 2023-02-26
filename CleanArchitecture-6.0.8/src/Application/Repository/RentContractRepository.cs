using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class RentContractRepository : IRentContractRepository
    {
        private readonly ContractContext _contractContext;

        public RentContractRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public RentContract GetRentContractById(int id)
        {
            return _contractContext.RentContracts.Where(c => c.Id == id).FirstOrDefault();
        }

        public RentContract GetRentContractByContractGroupId(int contractGroupId)
        {
            return _contractContext.RentContracts.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();
        }


        public bool RentContractExit(int id)
        {
            return _contractContext.RentContracts.Any(c => c.Id == id);
        }

        public void CreateRentContract(RentContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExported;


            var rentContract = new RentContract
            {
                ContractGroupId = request.ContractGroupId,
                RepresentativeId = request.RepresentativeId,
                DeliveryAddress = request.DeliveryAddress,
                CarGeneralInfoAtRentPriceForNormalDay = request.CarGeneralInfoAtRentPriceForNormalDay,
                CarGeneralInfoAtRentPriceForWeekendDay = request.CarGeneralInfoAtRentPriceForWeekendDay,
                CarGeneralInfoAtRentPriceForHoliday = request.CarGeneralInfoAtRentPriceForHoliday,
                CarGeneralInfoAtRentPricePerKmExceed = request.CarGeneralInfoAtRentPricePerKmExceed,
                CarGeneralInfoAtRentPricePerHourExceed = request.CarGeneralInfoAtRentPricePerHourExceed,
                CarGeneralInfoAtRentLimitedKmForMonth = request.CarGeneralInfoAtRentLimitedKmForMonth,
                CarGeneralInfoAtRentPriceForMonth = request.CarGeneralInfoAtRentPriceForMonth,
                DownPayment = request.DownPayment ,
                CreatedDate = request.CreatedDate,
                PaymentAmount = request.PaymentAmount,
                DepositItemAsset = request.DepositItemAsset ,
                DepositItemDescription = request.DepositItemDescription ,
                DepositItemDownPayment = request.DepositItemDownPayment ,
                ContractStatusId = defaultContractId
            };
            _contractContext.RentContracts.Add(rentContract);
            _contractContext.SaveChanges();

        }

        public void UpdateRentContract(int id, RentContractUpdateModel request)
        {
            var rentContract = _contractContext.RentContracts.Find(id);
            rentContract.ContractGroupId = request.ContractGroupId;
            rentContract.RepresentativeId = request.RepresentativeId;
            rentContract.DeliveryAddress = request.DeliveryAddress;
            rentContract.CarGeneralInfoAtRentPriceForNormalDay = request.CarGeneralInfoAtRentPriceForNormalDay;
            rentContract.CarGeneralInfoAtRentPriceForWeekendDay = request.CarGeneralInfoAtRentPriceForWeekendDay;
            rentContract.CarGeneralInfoAtRentPriceForHoliday = request.CarGeneralInfoAtRentPriceForHoliday;
            rentContract.CarGeneralInfoAtRentPricePerKmExceed = request.CarGeneralInfoAtRentPricePerKmExceed;
            rentContract.CarGeneralInfoAtRentPricePerHourExceed = request.CarGeneralInfoAtRentPricePerHourExceed;
            rentContract.CarGeneralInfoAtRentLimitedKmForMonth = request.CarGeneralInfoAtRentLimitedKmForMonth;
            rentContract.CarGeneralInfoAtRentPriceForMonth = request.CarGeneralInfoAtRentPriceForMonth;
            rentContract.DownPayment = request.DownPayment;
            rentContract.CustomerSignature = request.CustomerSignature;
            rentContract.StaffSignature = request.StaffSignature;
            rentContract.FilePath = request.FilePath;
            rentContract.FileWithSignsPath = request.FileWithSignsPath;
            rentContract.IsExported = request.IsExported;
            rentContract.PaymentAmount = request.PaymentAmount;
            rentContract.DepositItemAsset = request.DepositItemAsset;
            rentContract.DepositItemDescription = request.DepositItemDescription;
            rentContract.DepositItemDownPayment = request.DepositItemDownPayment;
            rentContract.ContractStatusId = request.ContractStatusId;

            _contractContext.RentContracts.Update(rentContract);
            _contractContext.SaveChanges();
        }

        public bool UpdateRentContractStatus(int id, RentContractUpdateStatusModel request)
        {
            var rentContract = _contractContext.RentContracts.Where(c => c.Id == id).FirstOrDefault();

            if (rentContract == null)
                return false;

            rentContract.ContractStatusId = request.ContractStatusId;
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }

}
