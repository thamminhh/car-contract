using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Interface;
using MediatR;

namespace CleanArchitecture.Application.Repository
{
    public class RentContractRepository : IRentContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly FileRepository _fileRepository;
        private readonly IContractGroupRepository _contractGroupRepository;
        public RentContractRepository(ContractContext contractContext, FileRepository fileRepository, IContractGroupRepository contractGroupRepository)
        {
            _contractContext = contractContext;
            _fileRepository = fileRepository;
            _contractGroupRepository = contractGroupRepository;
        }

        public RentContract GetRentContractById(int id)
        {
            var rentContract = _contractContext.RentContracts.Where(c => c.Id == id).FirstOrDefault();
            var host = _fileRepository.GetCurrentHost();
            return new RentContract
            {
                Id = id,
                ContractGroupId = rentContract.ContractGroupId,
                RepresentativeId = rentContract.RepresentativeId,
                DeliveryAddress = rentContract.DeliveryAddress,
                CarGeneralInfoAtRentPriceForNormalDay = rentContract.CarGeneralInfoAtRentPriceForNormalDay,
                CarGeneralInfoAtRentPriceForWeekendDay = rentContract.CarGeneralInfoAtRentPriceForWeekendDay,
                CarGeneralInfoAtRentPriceForHoliday = rentContract.CarGeneralInfoAtRentPriceForHoliday,
                CarGeneralInfoAtRentPricePerKmExceed = rentContract.CarGeneralInfoAtRentPricePerKmExceed,
                CarGeneralInfoAtRentPricePerHourExceed = rentContract.CarGeneralInfoAtRentPricePerHourExceed,
                CarGeneralInfoAtRentLimitedKmForMonth = rentContract.CarGeneralInfoAtRentLimitedKmForMonth,
                CarGeneralInfoAtRentPriceForMonth = rentContract.CarGeneralInfoAtRentPriceForMonth,
                DownPayment = rentContract.DownPayment,
                CreatedDate = rentContract.CreatedDate,
                PaymentAmount = rentContract.PaymentAmount,
                DepositItemAsset = rentContract.DepositItemAsset,
                DepositItemDescription = rentContract.DepositItemDescription,
                DepositItemDownPayment = rentContract.DepositItemDownPayment,
                FilePath = host + rentContract.FilePath,
                ContractStatusId = rentContract.ContractStatusId
            };
        }

        public RentContract GetRentContractByContractGroupId(int contractGroupId)
        {
            var rentContract = _contractContext.RentContracts.Where(c => c.ContractGroupId == contractGroupId).FirstOrDefault();
            var host = _fileRepository.GetCurrentHost();
            return new RentContract
            {
                Id = rentContract.Id,
                ContractGroupId = rentContract.ContractGroupId,
                RepresentativeId = rentContract.RepresentativeId,
                DeliveryAddress = rentContract.DeliveryAddress,
                CarGeneralInfoAtRentPriceForNormalDay = rentContract.CarGeneralInfoAtRentPriceForNormalDay,
                CarGeneralInfoAtRentPriceForWeekendDay = rentContract.CarGeneralInfoAtRentPriceForWeekendDay,
                CarGeneralInfoAtRentPriceForHoliday = rentContract.CarGeneralInfoAtRentPriceForHoliday,
                CarGeneralInfoAtRentPricePerKmExceed = rentContract.CarGeneralInfoAtRentPricePerKmExceed,
                CarGeneralInfoAtRentPricePerHourExceed = rentContract.CarGeneralInfoAtRentPricePerHourExceed,
                CarGeneralInfoAtRentLimitedKmForMonth = rentContract.CarGeneralInfoAtRentLimitedKmForMonth,
                CarGeneralInfoAtRentPriceForMonth = rentContract.CarGeneralInfoAtRentPriceForMonth,
                DownPayment = rentContract.DownPayment,
                CreatedDate = rentContract.CreatedDate,
                PaymentAmount = rentContract.PaymentAmount,
                DepositItemAsset = rentContract.DepositItemAsset,
                DepositItemDescription = rentContract.DepositItemDescription,
                DepositItemDownPayment = rentContract.DepositItemDownPayment,
                FilePath = host + rentContract.FilePath,
                ContractStatusId = rentContract.ContractStatusId
            };
        }


        public bool RentContractExit(int id)
        {
            return _contractContext.RentContracts.Any(c => c.Id == id);
        }

        public void CreateRentContract(RentContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExporting;

            string htmlContent = "<h1> Hợp đồng thuê </h1>";
            string fileName = "RentContract" + ".pdf";



            htmlContent += "<h2> RepresentativeId: " + request.RepresentativeId + "</h2>";
            htmlContent += "<h2> ContractGroupId: " + request.ContractGroupId + "</h2>";
            htmlContent += "<h2> DeliveryAddress: " + request.DeliveryAddress + "</h2>";
            htmlContent += "<h2> CreatedDate: " + request.CreatedDate + "</h2>";

            var file = _fileRepository.GeneratePdfAsync(htmlContent, fileName);

            var filePath = _fileRepository.SaveFileToFolder(file, "1");


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
                FilePath = filePath,
                ContractStatusId = defaultContractId
            };
            _contractContext.RentContracts.Add(rentContract);
            _contractContext.SaveChanges();

            var contractGroupStatusExpertised = Constant.ContractGroupConstant.RentContractNotSign;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusExpertised;

            _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

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
