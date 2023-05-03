using System.Net.NetworkInformation;
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSchedules.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractStatistic.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CleanArchitecture.Application.Repository
{
    public class RentContractRepository : IRentContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly FileRepository _fileRepository;
        private readonly IContractGroupRepository _contractGroupRepository;
        private readonly IAppraisalRecordRepository _appraisalRecordRepository;
        private readonly ICarScheduleRepository _carScheduleRepository;
        private readonly IContractStatisticRepository _contractStatisticRepository;
        public RentContractRepository(ContractContext contractContext, FileRepository fileRepository,
            IContractGroupRepository contractGroupRepository, IAppraisalRecordRepository appraisalRecordRepository, ICarScheduleRepository carScheduleRepository,
            IContractStatisticRepository contractStatisticRepository)
        {
            _contractContext = contractContext;
            _fileRepository = fileRepository;
            _contractGroupRepository = contractGroupRepository;
            _appraisalRecordRepository = appraisalRecordRepository;
            _carScheduleRepository = carScheduleRepository;
            _contractStatisticRepository = contractStatisticRepository;
        }

        public RentContractDataModel GetRentContractById(int id)
        {
            RentContract rentContract = _contractContext.RentContracts
                .Include(c => c.Representative)
                .FirstOrDefault(c => c.Id == id);
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == rentContract.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            return new RentContractDataModel
            {
                Id = rentContract.Id,
                ContractGroupId = rentContract.ContractGroupId,
                RepresentativeId = rentContract.RepresentativeId,
                RepresentativeName = rentContract.Representative.Name,
                RepresentativePhoneNumber = rentContract.Representative.PhoneNumber,
                RepresentativeAddress = rentContract.Representative.CurrentAddress,

                CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,

                CarModel = car.CarModel.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,
                RentFrom = contractGroup.RentFrom,
                RentTo = contractGroup.RentTo,

                DeliveryAddress = rentContract.DeliveryAddress,
                DeliveryFee = rentContract.DeliveryFee,
                CarGeneralInfoAtRentPriceForNormalDay = rentContract.CarGeneralInfoAtRentPriceForNormalDay,
                CarGeneralInfoAtRentPriceForWeekendDay = rentContract.CarGeneralInfoAtRentPriceForWeekendDay,
                CarGeneralInfoAtRentPricePerKmExceed = rentContract.CarGeneralInfoAtRentPricePerKmExceed,
                CarGeneralInfoAtRentPricePerHourExceed = rentContract.CarGeneralInfoAtRentPricePerHourExceed,
                CarGeneralInfoAtRentLimitedKmForMonth = rentContract.CarGeneralInfoAtRentLimitedKmForMonth,
                CarGeneralInfoAtRentPriceForMonth = rentContract.CarGeneralInfoAtRentPriceForMonth,
                CreatedDate = rentContract.CreatedDate,
                PaymentAmount = rentContract.PaymentAmount,
                DepositItemDownPayment = rentContract.DepositItemDownPayment,

                FilePath = host + rentContract.FilePath,
                ContractStatusId = rentContract.ContractStatusId,
                StaffSignature = rentContract.StaffSignature,
                CustomerSignature = rentContract.CustomerSignature,
                FileWithSignsPath = host + rentContract.FileWithSignsPath,
                IsExported = rentContract.IsExported,
                CancelReason = rentContract.CancelReason,

            };
        }
        public RentContractDataModel GetLastRentContractByContractGroupId(int contractGroupId)
        {
            RentContract rentContract = _contractContext.RentContracts
                .Include(c => c.Representative)
                .Where(c => c.ContractGroupId == contractGroupId)
                .OrderByDescending(c => c.Id)
                .FirstOrDefault();
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == rentContract.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            return new RentContractDataModel
            {
                Id = rentContract.Id,
                ContractGroupId = rentContract.ContractGroupId,
                RepresentativeId = rentContract.RepresentativeId,
                RepresentativeName = rentContract.Representative.Name,
                RepresentativePhoneNumber = rentContract.Representative.PhoneNumber,
                RepresentativeAddress = rentContract.Representative.CurrentAddress,

                CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,

                CarModel = car.CarModel.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,
                RentFrom = contractGroup.RentFrom,
                RentTo = contractGroup.RentTo,

                DeliveryAddress = rentContract.DeliveryAddress,
                DeliveryFee = rentContract.DeliveryFee,
                CarGeneralInfoAtRentPriceForNormalDay = rentContract.CarGeneralInfoAtRentPriceForNormalDay,
                CarGeneralInfoAtRentPriceForWeekendDay = rentContract.CarGeneralInfoAtRentPriceForWeekendDay,
                CarGeneralInfoAtRentPricePerKmExceed = rentContract.CarGeneralInfoAtRentPricePerKmExceed,
                CarGeneralInfoAtRentPricePerHourExceed = rentContract.CarGeneralInfoAtRentPricePerHourExceed,
                CarGeneralInfoAtRentLimitedKmForMonth = rentContract.CarGeneralInfoAtRentLimitedKmForMonth,
                CarGeneralInfoAtRentPriceForMonth = rentContract.CarGeneralInfoAtRentPriceForMonth,
                CreatedDate = rentContract.CreatedDate,
                PaymentAmount = rentContract.PaymentAmount,
                DepositItemDownPayment = rentContract.DepositItemDownPayment,

                FilePath = host + rentContract.FilePath,
                ContractStatusId = rentContract.ContractStatusId,
                StaffSignature = rentContract.StaffSignature,
                CustomerSignature = rentContract.CustomerSignature,
                FileWithSignsPath = host + rentContract.FileWithSignsPath,
                IsExported = rentContract.IsExported,
                CancelReason = rentContract.CancelReason,
            };
        }

        public ICollection<RentContractDataModel> GetRentContractByContractGroupId(int contractGroupId)
        {
            var rentContracts = _contractContext.RentContracts.Where(c => c.ContractGroupId == contractGroupId).ToList();
            var host = _fileRepository.GetCurrentHost();

            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == contractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var rentContractDataModels = rentContracts
                .OrderBy(c => c.Id)
                .Select(c => new RentContractDataModel
                {
                    Id = c.Id,
                    ContractGroupId = c.ContractGroupId,
                    RepresentativeId = c.RepresentativeId,
                    CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
                    CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                    CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                    CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
                    CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,

                    CarModel = car.CarModel.Name,
                    CarLicensePlates = car.CarLicensePlates,
                    SeatNumber = car.SeatNumber,
                    RentFrom = contractGroup.RentFrom,
                    RentTo = contractGroup.RentTo,

                    DeliveryAddress = c.DeliveryAddress,
                    DeliveryFee = c.DeliveryFee,
                    CarGeneralInfoAtRentPriceForNormalDay = c.CarGeneralInfoAtRentPriceForNormalDay,
                    CarGeneralInfoAtRentPriceForWeekendDay = c.CarGeneralInfoAtRentPriceForWeekendDay,
                    CarGeneralInfoAtRentPricePerKmExceed = c.CarGeneralInfoAtRentPricePerKmExceed,
                    CarGeneralInfoAtRentPricePerHourExceed = c.CarGeneralInfoAtRentPricePerHourExceed,
                    CarGeneralInfoAtRentLimitedKmForMonth = c.CarGeneralInfoAtRentLimitedKmForMonth,
                    CarGeneralInfoAtRentPriceForMonth = c.CarGeneralInfoAtRentPriceForMonth,
                    CreatedDate = c.CreatedDate,
                    PaymentAmount = c.PaymentAmount,
                    DepositItemDownPayment = c.DepositItemDownPayment,

                    FilePath = host + c.FilePath,
                    ContractStatusId = c.ContractStatusId,
                    StaffSignature = c.StaffSignature,
                    CustomerSignature = c.CustomerSignature,
                    FileWithSignsPath = host + c.FileWithSignsPath,
                    IsExported = c.IsExported,
                    CancelReason = c.CancelReason,

                }).ToList();

            return rentContractDataModels;

        }

        public bool RentContractExit(int id)
        {
            return _contractContext.RentContracts.Any(c => c.Id == id);
        }

        public void CreateRentContract(RentContractCreateModel request)
        {
            //Create PDF file and upload in wwwroot

            string htmlContent = CreateRentContractContent(request);
            var file = _fileRepository.GeneratePdfAsync(htmlContent);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());

            //Create RentContract in db
            var defaultContractId = ContractStatusConstant.ContractExporting;
            var appraisalRecord = _appraisalRecordRepository.GetLastAppraisalRecordByContractGroupId(request.ContractGroupId);
            var rentContract = new RentContract
            {
                ContractGroupId = request.ContractGroupId,
                RepresentativeId = request.RepresentativeId,
                DeliveryAddress = request.DeliveryAddress,
                CarGeneralInfoAtRentPriceForNormalDay = request.CarGeneralInfoAtRentPriceForNormalDay,
                CarGeneralInfoAtRentPriceForWeekendDay = request.CarGeneralInfoAtRentPriceForWeekendDay,
                CarGeneralInfoAtRentPricePerKmExceed = request.CarGeneralInfoAtRentPricePerKmExceed,
                CarGeneralInfoAtRentPricePerHourExceed = request.CarGeneralInfoAtRentPricePerHourExceed,
                CarGeneralInfoAtRentLimitedKmForMonth = request.CarGeneralInfoAtRentLimitedKmForMonth,
                CarGeneralInfoAtRentPriceForMonth = request.CarGeneralInfoAtRentPriceForMonth,
                DeliveryFee = request.DeliveryFee,
                CreatedDate = request.CreatedDate,
                PaymentAmount = request.PaymentAmount,
                DepositItemDownPayment = appraisalRecord.DepositInfoDownPayment,
                FilePath = filePath,
                ContractStatusId = defaultContractId
            };
            _contractContext.RentContracts.Add(rentContract);
            _contractContext.SaveChanges();

            //Update ContractGroup status 
            var contractGroupStatusExpertised = Constant.ContractGroupConstant.RentContractNotSign;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusExpertised;
            _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

            //Create ContractStatistic 
            var contractStatistic = new ContractStatisticCreateModel();
            contractStatistic.ContractGroupId = request.ContractGroupId;
            contractStatistic.EtcmoneyUsing = 0;
            contractStatistic.FuelMoneyUsing = 0;
            contractStatistic.ExtraTimeMoney = 0;
            contractStatistic.ExtraKmMoney = 0;
            contractStatistic.InsuranceMoney = 0;
            contractStatistic.ViolationMoney = 0;
            contractStatistic.PaymentAmount = request.PaymentAmount;
            _contractStatisticRepository.CreateContractStatistic(contractStatistic);

            //Create 
        }

        public void UpdateRentContract(int id, RentContractUpdateModel request)
        {
            //Create PDF file and upload in wwwroot
            string htmlContent = UpdateRentContractContent(request);
            var file = _fileRepository.GeneratePdfAsync(htmlContent);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());
            //Update RentContract
            var rentContract = _contractContext.RentContracts.Find(id);

            rentContract.ContractGroupId = request.ContractGroupId;
            rentContract.RepresentativeId = request.RepresentativeId;
            rentContract.DeliveryAddress = request.DeliveryAddress;
            rentContract.CarGeneralInfoAtRentPriceForNormalDay = request.CarGeneralInfoAtRentPriceForNormalDay;
            rentContract.CarGeneralInfoAtRentPriceForWeekendDay = request.CarGeneralInfoAtRentPriceForWeekendDay;
            rentContract.CarGeneralInfoAtRentPricePerKmExceed = request.CarGeneralInfoAtRentPricePerKmExceed;
            rentContract.CarGeneralInfoAtRentPricePerHourExceed = request.CarGeneralInfoAtRentPricePerHourExceed;
            rentContract.CarGeneralInfoAtRentLimitedKmForMonth = request.CarGeneralInfoAtRentLimitedKmForMonth;
            rentContract.CarGeneralInfoAtRentPriceForMonth = request.CarGeneralInfoAtRentPriceForMonth;
            rentContract.DeliveryFee = request.DeliveryFee;

            rentContract.CustomerSignature = request.CustomerSignature;
            rentContract.StaffSignature = request.StaffSignature;

            rentContract.IsExported = request.IsExported;
            rentContract.PaymentAmount = request.PaymentAmount;
            rentContract.CancelReason = request.CancelReason;
            rentContract.CustomerSignature = request.CustomerSignature;
            rentContract.StaffSignature = request.StaffSignature;

            if (request.StaffSignature != null)
            {
                rentContract.ContractStatusId = Constant.ContractStatusConstant.ContractExporting;
            }
            else
            {
                rentContract.ContractStatusId = request.ContractStatusId;
            }
            if (request.CancelReason != null)
            {
                rentContract.ContractStatusId = Constant.ContractStatusConstant.ContractCancelled;
            }
            rentContract.FileWithSignsPath = filePath;

            _contractContext.RentContracts.Update(rentContract);
            _contractContext.SaveChanges();


            if (request.CancelReason != null)
            {
                var contractGroupStatusRentCancel = Constant.ContractGroupConstant.RentContractCancel;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusRentCancel;
                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
            }
            if (request.StaffSignature != null)
            {
                //Update ContractGroupStatus
                var contractGroupStatusRentNotSign = Constant.ContractGroupConstant.RentContractNotSign;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusRentNotSign;
                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

                //get ContractGroup 
                var contractGroup = _contractContext.ContractGroups
                .FirstOrDefault(c => c.Id == request.ContractGroupId);

                //Create CarSchedule
                var carRentStatus = Constant.CarStatusConstants.Renting;
                var carScheduleCreateModel = new CarScheduleCreateModel();
                carScheduleCreateModel.CarId = contractGroup.CarId;
                carScheduleCreateModel.DateStart = contractGroup.RentFrom;
                carScheduleCreateModel.DateEnd = contractGroup.RentTo;
                carScheduleCreateModel.CarStatusId = carRentStatus;
                
                _carScheduleRepository.CreateCarSchedule(carScheduleCreateModel);
            }
        }

        public bool UpdateRentContractStatus(int id, RentContractUpdateStatusModel request)
        {
            var rentContract = _contractContext.RentContracts.Where(c => c.Id == id).FirstOrDefault();

            if (rentContract == null)
                return false;

            rentContract.ContractStatusId = request.ContractStatusId;
            return Save();
        }
        public bool UpdateRentContractSigned(int id, RentContractUpdateSignedModel request)
        {
            var rentContract = _contractContext.RentContracts.Where(c => c.Id == id).FirstOrDefault();

            if (rentContract == null)
                return false;

            rentContract.FileWithSignsPath = request.FileWithSignPath;
            return Save();
        }

        public string CreateRentContractContent(RentContractCreateModel request)
        {
            var representer = _contractContext.Users.FirstOrDefault(c => c.Id == request.RepresentativeId);
            var contractGroup = _contractContext.ContractGroups
               .Include(c => c.CustomerInfo)
               .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
            .Include(c => c.CarModel)
            .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var appraisalRecord = _appraisalRecordRepository.GetLastAppraisalRecordByContractGroupId(request.ContractGroupId);
            var parkingLot = _contractContext.ParkingLots.FirstOrDefault(c => c.Id == car.ParkingLotId);
            double? total = request.PaymentAmount + appraisalRecord.DepositInfoDownPayment + request.DeliveryFee;

            string htmlContent = "<ul>";
            htmlContent += "<table style='width: 100%;'>";
            htmlContent += "<tr style='margin-left: 5px; list-style-type: none; width:50%;'>";
            htmlContent += "<td>";
            htmlContent += "<img src='https://amazingtech.vn/Content/amazingtech/assets/img/logo-color.png' style='height: 60px;'/>";
            htmlContent += "<li>CÔNG TY CP CÔNG NGHỆ ATSHARE</li>";
            htmlContent += "<li style='margin-left: 10px; margin-top: -15px;'>Số: " + request.ContractGroupId + "/Atshare</li>";
            htmlContent+= "</td>";
            htmlContent+= "<td style='height: 50px;'>";
            htmlContent += "<li style='text-align: center; font-weight: 700; font-size: 16px;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</li>";
            htmlContent += "<li style='text-align: center;  margin-top: -15px;'>Độc lập – Tự do – Hạnh phúc</li>";
            htmlContent += "</td>";
            htmlContent += "</tr>";
            htmlContent += "</table>";
            htmlContent += "</ul>";
            htmlContent += "<h1 style='text-align: center;'>HỢP ĐỒNG CHO THUÊ XE </h1>";
            htmlContent += "<ul><li>Căn cứ Bộ Luật dân sự số 91/2015/QH13 nước CHXHCN Việt Nam ngày 01/01/2017</li>";
            htmlContent += "<li>Căn cứ Luật thương mại số 36/2005/QH11 nước CHXHCN Việt Nam ngày 26/06/2005</li>";
            htmlContent += "<li>Căn cứ vào khả năng cung cấp và nhu cầu của hai bên.</li></ul>";
            htmlContent += "<p>Hôm nay, ngày " + request.CreatedDate + ", chúng tôi gồm:</p>";

            htmlContent += " <h2>BÊN CHO THUÊ XE (BÊN A): CÔNG TY CỔ PHẦN CÔNG NGHỆ ATSHARE</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ: "+ parkingLot.Address + "</li>";
            htmlContent += "<li>MST:0110032942</li> ";
            htmlContent += "<li>STK:19035651301016 tại NH Kỹ Thương VN (Techcombank), CN Hai Bà Trưng</li>";
            htmlContent += "<li>Đại diện: " + representer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + representer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B): " + contractGroup.CustomerInfo.CustomerName + "</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + "</li>";
            htmlContent += "<li>Cấp ngày: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Sau khi bàn bạc, thỏa thuận, hai bên cùng nhất trí ký hợp đồng thuê xe với các điều khoản sau:</p>";
            htmlContent += "<h2>Điều I: Nội dung hợp đồng</h2>";
            htmlContent += "<p>Bên A đồng ý cho Bên B thuê xác xe ô tô để phục vụ mục đích đi lại:</p>";
            htmlContent += "<ul>";
            htmlContent += "<li>Hiệu xe: " + car.CarModel.Name + "</li>";
            htmlContent += "<li>Biển số: " + car.CarLicensePlates + "</li>";
            htmlContent += "<li>Bắt đầu từ: " + contractGroup.RentFrom + " </li>";
            htmlContent += "<li>Đến: " + contractGroup.RentTo + "</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>Điều II: Thanh toán (giá chưa bao gồm VAT)</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Giới hạn hành trình: " + request.CarGeneralInfoAtRentLimitedKmForMonth + " Km</li>";
            htmlContent += "<li>Đặt cọc hợp đồng: " + appraisalRecord.DepositInfoDownPayment + " VNĐ </li>";
            htmlContent += "<li>Phí phát sinh Km: " + request.CarGeneralInfoAtRentPricePerKmExceed + "/Km</li>";
            htmlContent += "<li>Phí phát sinh thời gian: " + request.CarGeneralInfoAtRentPricePerHourExceed + " VNĐ/giờ </li>";
            htmlContent += "<li>Phí giao xe: " + request.DeliveryFee + " VNĐ </li>";
            htmlContent += "<li>Tiền thuê xe: " + request.PaymentAmount + " VNĐ </li>";
            htmlContent += "<li>Thanh toán khi nhận xe: " + total + " VNĐ</li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>Điều III: Quyền hạn trách nhiệm của Bên A</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>1. Bên A có trách nhiệm giao xe cho bên B đúng chủng loại, hoạt động bình thường, địa điểm đã thỏa thuận và cung cấp thông tin cần thiết để sử dụng phương tiện. Trường hợp bất khả kháng, xe xảy ra sự cố thì bên A sẽ thay thế xe có giá trị tương đương để bên B sử dụng.</li>";
            htmlContent += "<li>2. Cung cấp đầy đủ các giấy tờ bao gồm: Đăng ký xe (1), Kiểm định xe (2), Bảo hiểm xe (3)</li>";
            htmlContent += "<li>3. Bên A có quyền yêu cầu bên B trả xe trước thời hạn hợp đồng nếu bên A phát hiện bên B sử dụng sai mục đích hoặc bên B vi phạm các điều khoản đã thỏa thuận trong hợp đồng này.</li>";
            htmlContent += "<li>4. Bên A có quyền sử dụng tài sản thế chấp của bên B để thanh toán hợp đồng (trong trường hợp bên B thực hiện không đúng hoặc không đủ nghĩa vụ của hợp đồng với bên A) và các chi phí phát sinh khác. Nếu giá trị tài sản thế chấp của bên B thấp hơn các chi phí phát sinh thì bên B có trách nhiệm thanh toán thêm bằng tiền mặt.<li>";
            htmlContent += "<li>5. Bên A có quyền đơn phương chấm dứt hợp đồng nếu bên B (hoặc tài xế) sử dụng xe không thành thạo, hoặc thành thạo nhưng không đảm bảo an toàn như : say rượu, sử dụng chất kích thích... Mọi chi phí bên thuê xe vẫn phải thanh toán trong trường hợp này.</li>";
            htmlContent += "<li>6. Trong thời gian thuê xe, nếu bên A không liên lạc được với bên B (trong vòng 12 giờ), bên A được quyền nhờ cơ quan chức năng tìm kiếm và thu hồi xe về, bên B phải chịu chi phí thiệt hại này.</li>";
            htmlContent += "<li>7. Khi bên A phát hiện bên B chạy quá tốc độ bằng phương pháp nghiệp vụ như theo dõi trên thiết bị định vị GPS (lắp trên xe) hoặc do phía cơ quan chức năng cung cấp. Bên B có thể đơn phương chấm dứt hợp đồng luôn với bên A tại thời điểm đó, toàn bộ chi phí cọc và tiền phát sinh bên B sẽ chịu toàn bộ chi phí.</li>";
            htmlContent += "<li>8. Nếu bên B bị phạt nguội mà trước hoặc sau khi kết thúc hợp đồng bên A vẫn có quyền phạt bên B. Mọi chi phí phát sinh như đi lại, ăn uống, nhà nghỉ, phí phạt, bên B sẽ thanh toán toàn bộ cho bên A.</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>Điều IV: Quyền hạn và trách nhiệm bên B</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>1. Khi xảy ra sự cố và va quệt làm hư hỏng xe, bên B phải thông báo và bồi thường theo hiện trạng cũ mà nơi sửa chữa do bên A quy định. Khi sửa chữa, bên B có trách nhiệm bồi thường thiệt hại về tiền cho bên A khi xe không lưu hành được theo đơn giá của ngày thuê xe. (Số ngày xe sửa chữa x Đơn giá thuê).\r\n</li>";
            htmlContent += "<li>2. Nếu Bên B trả xe mà không có hoặc làm mất giấy tờ xe (được quy định tại điều III, khoản 2) hoặc vi phạm luật an toàn giao thông đường bộ dẫn đến bị thu xe thì Bên B vẫn phải thanh toán tiền thuê xe bình thường cho đến khi trao trả giấy tờ xe và xe đầy đủ.\r\n</li>";
            htmlContent += "<li>3. Bên B vui lòng trả xe tại bãi xe của công ty.\r\n</li>";
            htmlContent += "<li>4. Nếu trong trường hợp bên B làm mất xe thì bên B phải chịu bồi thường 100% giá trị ban đầu của xe cho bên A.</li>";
            htmlContent += "<li>5. Trong quá trình sử dụng xe bên B chịu hoàn toàn trách nhiệm dân sự, hình sự, và luật lệ an toàn giao thông trước pháp luật nếu có phát sinh bất cứ chi phí phạt nào tại thời điểm bên B thuê xe, bên B vẫn chịu chi phí đó mặc dù hợp đồng đã thanh lý. Bên B tuân thủ đi đúng hành trình đã cam kết với bên A, nếu có thay đổi phải báo cho bên A biết. Nếu không bên A có quyền đơn phương chấm dứt hợp đồng, lấy xe về trước thời hạn.</li>";
            htmlContent += "<li>6. Hết hạn hợp đồng bên B trả xe ngay cho bên A (như tình trạng xe khi bàn giao). Thời gian Bên A nhận xe từ Bên B không muộn hơn giờ quy định ở trên, nếu trả xe sau giờ quy định Bên A sẽ tính phí phát sinh : 100.000 vnđ /1 giờ. Trường hợp Bên B trả xe sau 22h00 bên A sẽ tính chi phí phát sinh là 1 ngày.</li>";
            htmlContent += "<li>7. Trường hợp xe phát sinh : Khi phát sinh trả trước hạn hợp đồng bên B phải báo bên A trước 24h. Trường hợp Bên B không báo trước, bên A sẽ thu phí theo giá trị Hợp Đồng và không hoàn trả tiền thừa. Khi phát sinh trả sau hạn hợp đồng, Bên B đi thêm, Bên B phải báo bên A trước 8 tiếng so với thời gian hết hạn. Trường hợp Bên B báo muộn sau 8 tiếng chi phí phát sinh cho ngày mới tăng 30% giá thuê.</li>";
            htmlContent += "<li>8. Bên B không giao xe cho người khác sử dụng dưới bất kì hình thức nào hoặc chuyên chở các loại vũ khí, chất cháy nổ, hàng quốc cấm cũng như các đồ hải sản, đồ ăn, nước chấm, mắm hoặc hàng nặng mùi. Nếu vi phạm sẽ bị phạt từ 1.000.000 vnđ đến 5.000.000 vnđ cũng như toàn bộ chi phí khắc phục và giá trị ngày xe không khai thác kinh doanh được.</li>";
            htmlContent += "<li>9. Theo nghị định mới của Bộ GTVT về việc theo dõi hiện trường giao thông bằng Camera: Bên B sử dụng vi phạm luật GTĐB, gây tai nạn cho người tham gia giao thông, bỏ trốn khỏi hiện trường, vì lý do thực tế chưa xử lý ngay được, sau một thời gian bị phát hiện hoặc Cơ quan Pháp luật điều tra được thì vẫn phải chịu trách nhiệm trước Bên A và cơ quan pháp luật mặc dù hợp đồng kết thúc, Bên B đã trả xe.</li>";
            htmlContent += "<li>10. Mọi sự cố do Bên B gây ra, không thể tự giải quyết được phải nhờ đến Bên A trực tiếp giải quyết giúp, thì tất cả chi phí đi lại, ăn, nghỉ của Bên A do Bên B thanh toán.</li>";
            htmlContent += "<li>11. Không sử dụng chất kích thích khi lái xe, nếu vi phạm bên B sẽ chịu toàn bộ trách nhiệm trước pháp luật và sẽ chịu toàn bộ thiệt hại liên quan đến ngày nằm chờ gián đoạn kinh doanh, chi phí phát sinh nếu có.</li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>Điều V: Các thỏa thuận đặc biệt: </h2>";
            htmlContent += "<li>1. Bên B cam kết không thực hiện các hành vi:</li>";

            htmlContent += "<ul>";
            htmlContent += "<li>a. Sử dụng xe thuê vào mục đích cầm cố hay thế chấp, sử dụng xe sai mục đích. Không được lái xe ra khỏi lãnh thổ Việt Nam.</li>";
            htmlContent += "<li>b. Sử dụng xe thuê vào những mục đích phi pháp như: vận chuyển hàng hóa trái phép (ma túy, chất cấm, hàng lậu, những đối tượng bị truy nã...)</li>";
            htmlContent += "</ul>";

            htmlContent += "<li>2. Bên A có quyền:</li>";

            htmlContent += "<ul>";
            htmlContent += "<li>a. Báo cho cơ quan, gia đình Bên B, cơ quan điều tra nếu Bên B cố tình không liên lạc với Bên A</li>";
            htmlContent += "<li>b. Thanh lý tài sản thế chấp của Bên B, đền bù thiệt hại cho Bên A do bên B gây ra, nếu thiếu Bên A sẽ tiếp tục truy thu hoặc thực hiện các biện pháp theo quy định của pháp luật nhằm đảm bảo quyền lợi chính đáng của mình cho đến khi giải quyết xong thiệt hại;</li>";
            htmlContent += "<li>c. Bên A có quyền đơn phương hủy hợp đồng nếu thấy lái xe Bên B hoặc Bên B không đảm bảo kỹ thuật- chất lượng cho xe, cho an toàn giao thông;</li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>Điều VI: Cam kết chung: </h2>";

            htmlContent += "<ul>";
            htmlContent += "<li>1. Hai bên cam kết thực hiện đúng các quy định trong hợp đồng. Trong trường hợp xảy ra tranh chấp, hai bên chủ động cùng nhau thương lượng, giải quyết. Nếu không thành công thì hai bên cùng đưa vụ việc ra tòa án có thẩm quyền giải quyết.</li>";
            htmlContent += "<li>2. Hợp đồng này được lập thành 02 bản có giá trị pháp lý như nhau và có hiệu lực kể từ ngày ký.</li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>&nbsp;&nbsp;&nbsp;&nbsp;BÊN A &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
                ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "BÊN B</h3>";

            return htmlContent;
        }

        public string UpdateRentContractContent(RentContractUpdateModel request)
        {

            var representer = _contractContext.Users.FirstOrDefault(c => c.Id == request.RepresentativeId);
            var contractGroup = _contractContext.ContractGroups
               .Include(c => c.CustomerInfo)
               .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
            .Include(c => c.CarModel)
            .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var appraisalRecord = _appraisalRecordRepository.GetLastAppraisalRecordByContractGroupId(request.ContractGroupId);
            var parkingLot = _contractContext.ParkingLots.FirstOrDefault(c => c.Id == car.ParkingLotId);
            double? total = request.PaymentAmount + appraisalRecord.DepositInfoDownPayment + request.DeliveryFee;

            string htmlContent = "<ul>";
            htmlContent += "<table style='width: 100%;'>";
            htmlContent += "<tr style='margin-left: 5px; list-style-type: none; width:50%;'>";
            htmlContent += "<td>";
            htmlContent += "<img src='https://amazingtech.vn/Content/amazingtech/assets/img/logo-color.png' style='height: 60px;'/>";
            htmlContent += "<li>CÔNG TY CP CÔNG NGHỆ ATSHARE</li>";
            htmlContent += "<li style='margin-left: 10px; margin-top: -15px;'>Số: " + request.ContractGroupId + "/Atshare</li>";
            htmlContent += "</td>";
            htmlContent += "<td style='height: 50px;'>";
            htmlContent += "<li style='text-align: center; font-weight: 700; font-size: 16px;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</li>";
            htmlContent += "<li style='text-align: center;  margin-top: -15px;'>Độc lập – Tự do – Hạnh phúc</li>";
            htmlContent += "</td>";
            htmlContent += "</tr>";
            htmlContent += "</table>";
            htmlContent += "</ul>";
            htmlContent += "<h1 style='text-align: center;'>HỢP ĐỒNG CHO THUÊ XE </h1>";
            htmlContent += "<ul><li>Căn cứ Bộ Luật dân sự số 91/2015/QH13 nước CHXHCN Việt Nam ngày 01/01/2017</li>";
            htmlContent += "<li>Căn cứ Luật thương mại số 36/2005/QH11 nước CHXHCN Việt Nam ngày 26/06/2005</li>";
            htmlContent += "<li>Căn cứ vào khả năng cung cấp và nhu cầu của hai bên.</li></ul>";
            htmlContent += "<p>Hôm nay, ngày " + DateTime.Today + ", chúng tôi gồm:</p>";

            htmlContent += " <h2>BÊN CHO THUÊ XE (BÊN A): CÔNG TY CỔ PHẦN CÔNG NGHỆ ATSHARE</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ: " + parkingLot.Address + "</li>";
            htmlContent += "<li>MST:0110032942</li> ";
            htmlContent += "<li>STK:19035651301016 tại NH Kỹ Thương VN (Techcombank), CN Hai Bà Trưng</li>";
            htmlContent += "<li>Đại diện: " + representer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + representer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B): " + contractGroup.CustomerInfo.CustomerName + "</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + "</li>";
            htmlContent += "<li>Cấp ngày: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Sau khi bàn bạc, thỏa thuận, hai bên cùng nhất trí ký hợp đồng thuê xe với các điều khoản sau:</p>";
            htmlContent += "<h2>Điều I: Nội dung hợp đồng</h2>";
            htmlContent += "<p>Bên A đồng ý cho Bên B thuê xác xe ô tô để phục vụ mục đích đi lại:</p>";
            htmlContent += "<ul>";
            htmlContent += "<li>Hiệu xe: " + car.CarModel.Name + "</li>";
            htmlContent += "<li>Biển số: " + car.CarLicensePlates + "</li>";
            htmlContent += "<li>Bắt đầu từ: " + contractGroup.RentFrom + " </li>";
            htmlContent += "<li>Đến: " + contractGroup.RentTo + "</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>Điều II: Thanh toán (giá chưa bao gồm VAT)</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Giới hạn hành trình: " + request.CarGeneralInfoAtRentLimitedKmForMonth + " Km</li>";
            htmlContent += "<li>Đặt cọc hợp đồng: " + appraisalRecord.DepositInfoDownPayment + " VNĐ </li>";
            htmlContent += "<li>Phí phát sinh Km: " + request.CarGeneralInfoAtRentPricePerKmExceed + "/Km</li>";
            htmlContent += "<li>Phí phát sinh thời gian: " + request.CarGeneralInfoAtRentPricePerHourExceed + " VNĐ/giờ </li>";
            htmlContent += "<li>Phí giao xe: " + request.DeliveryFee + " VNĐ </li>";
            htmlContent += "<li>Tiền thuê xe: " + request.PaymentAmount + " VNĐ </li>";
            htmlContent += "<li>Thanh toán khi nhận xe: " + total + " VNĐ</li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>Điều III: Quyền hạn trách nhiệm của Bên A</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>1. Bên A có trách nhiệm giao xe cho bên B đúng chủng loại, hoạt động bình thường, địa điểm đã thỏa thuận và cung cấp thông tin cần thiết để sử dụng phương tiện. Trường hợp bất khả kháng, xe xảy ra sự cố thì bên A sẽ thay thế xe có giá trị tương đương để bên B sử dụng.</li>";
            htmlContent += "<li>2. Cung cấp đầy đủ các giấy tờ bao gồm: Đăng ký xe (1), Kiểm định xe (2), Bảo hiểm xe (3)</li>";
            htmlContent += "<li>3. Bên A có quyền yêu cầu bên B trả xe trước thời hạn hợp đồng nếu bên A phát hiện bên B sử dụng sai mục đích hoặc bên B vi phạm các điều khoản đã thỏa thuận trong hợp đồng này.</li>";
            htmlContent += "<li>4. Bên A có quyền sử dụng tài sản thế chấp của bên B để thanh toán hợp đồng (trong trường hợp bên B thực hiện không đúng hoặc không đủ nghĩa vụ của hợp đồng với bên A) và các chi phí phát sinh khác. Nếu giá trị tài sản thế chấp của bên B thấp hơn các chi phí phát sinh thì bên B có trách nhiệm thanh toán thêm bằng tiền mặt.<li>";
            htmlContent += "<li>5. Bên A có quyền đơn phương chấm dứt hợp đồng nếu bên B (hoặc tài xế) sử dụng xe không thành thạo, hoặc thành thạo nhưng không đảm bảo an toàn như : say rượu, sử dụng chất kích thích... Mọi chi phí bên thuê xe vẫn phải thanh toán trong trường hợp này.</li>";
            htmlContent += "<li>6. Trong thời gian thuê xe, nếu bên A không liên lạc được với bên B (trong vòng 12 giờ), bên A được quyền nhờ cơ quan chức năng tìm kiếm và thu hồi xe về, bên B phải chịu chi phí thiệt hại này.</li>";
            htmlContent += "<li>7. Khi bên A phát hiện bên B chạy quá tốc độ bằng phương pháp nghiệp vụ như theo dõi trên thiết bị định vị GPS (lắp trên xe) hoặc do phía cơ quan chức năng cung cấp. Bên B có thể đơn phương chấm dứt hợp đồng luôn với bên A tại thời điểm đó, toàn bộ chi phí cọc và tiền phát sinh bên B sẽ chịu toàn bộ chi phí.</li>";
            htmlContent += "<li>8. Nếu bên B bị phạt nguội mà trước hoặc sau khi kết thúc hợp đồng bên A vẫn có quyền phạt bên B. Mọi chi phí phát sinh như đi lại, ăn uống, nhà nghỉ, phí phạt, bên B sẽ thanh toán toàn bộ cho bên A.</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>Điều IV: Quyền hạn và trách nhiệm bên B</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>1. Khi xảy ra sự cố và va quệt làm hư hỏng xe, bên B phải thông báo và bồi thường theo hiện trạng cũ mà nơi sửa chữa do bên A quy định. Khi sửa chữa, bên B có trách nhiệm bồi thường thiệt hại về tiền cho bên A khi xe không lưu hành được theo đơn giá của ngày thuê xe. (Số ngày xe sửa chữa x Đơn giá thuê).\r\n</li>";
            htmlContent += "<li>2. Nếu Bên B trả xe mà không có hoặc làm mất giấy tờ xe (được quy định tại điều III, khoản 2) hoặc vi phạm luật an toàn giao thông đường bộ dẫn đến bị thu xe thì Bên B vẫn phải thanh toán tiền thuê xe bình thường cho đến khi trao trả giấy tờ xe và xe đầy đủ.\r\n</li>";
            htmlContent += "<li>3. Bên B vui lòng trả xe tại bãi xe của công ty.\r\n</li>";
            htmlContent += "<li>4. Nếu trong trường hợp bên B làm mất xe thì bên B phải chịu bồi thường 100% giá trị ban đầu của xe cho bên A.</li>";
            htmlContent += "<li>5. Trong quá trình sử dụng xe bên B chịu hoàn toàn trách nhiệm dân sự, hình sự, và luật lệ an toàn giao thông trước pháp luật nếu có phát sinh bất cứ chi phí phạt nào tại thời điểm bên B thuê xe, bên B vẫn chịu chi phí đó mặc dù hợp đồng đã thanh lý. Bên B tuân thủ đi đúng hành trình đã cam kết với bên A, nếu có thay đổi phải báo cho bên A biết. Nếu không bên A có quyền đơn phương chấm dứt hợp đồng, lấy xe về trước thời hạn.</li>";
            htmlContent += "<li>6. Hết hạn hợp đồng bên B trả xe ngay cho bên A (như tình trạng xe khi bàn giao). Thời gian Bên A nhận xe từ Bên B không muộn hơn giờ quy định ở trên, nếu trả xe sau giờ quy định Bên A sẽ tính phí phát sinh : 100.000 vnđ /1 giờ. Trường hợp Bên B trả xe sau 22h00 bên A sẽ tính chi phí phát sinh là 1 ngày.\r\n</li>";
            htmlContent += "<li>7. Trường hợp xe phát sinh : Khi phát sinh trả trước hạn hợp đồng bên B phải báo bên A trước 24h. Trường hợp Bên B không báo trước, bên A sẽ thu phí theo giá trị Hợp Đồng và không hoàn trả tiền thừa. Khi phát sinh trả sau hạn hợp đồng, Bên B đi thêm, Bên B phải báo bên A trước 8 tiếng so với thời gian hết hạn. Trường hợp Bên B báo muộn sau 8 tiếng chi phí phát sinh cho ngày mới tăng 30% giá thuê.</li>";
            htmlContent += "<li>8. Bên B không giao xe cho người khác sử dụng dưới bất kì hình thức nào hoặc chuyên chở các loại vũ khí, chất cháy nổ, hàng quốc cấm cũng như các đồ hải sản, đồ ăn, nước chấm, mắm hoặc hàng nặng mùi. Nếu vi phạm sẽ bị phạt từ 1.000.000 vnđ đến 5.000.000 vnđ cũng như toàn bộ chi phí khắc phục và giá trị ngày xe không khai thác kinh doanh được.\r\n</li>";
            htmlContent += "<li>9. Theo nghị định mới của Bộ GTVT về việc theo dõi hiện trường giao thông bằng Camera: Bên B sử dụng vi phạm luật GTĐB, gây tai nạn cho người tham gia giao thông, bỏ trốn khỏi hiện trường, vì lý do thực tế chưa xử lý ngay được, sau một thời gian bị phát hiện hoặc Cơ quan Pháp luật điều tra được thì vẫn phải chịu trách nhiệm trước Bên A và cơ quan pháp luật mặc dù hợp đồng kết thúc, Bên B đã trả xe.</li>";
            htmlContent += "<li>10. Mọi sự cố do Bên B gây ra, không thể tự giải quyết được phải nhờ đến Bên A trực tiếp giải quyết giúp, thì tất cả chi phí đi lại, ăn, nghỉ của Bên A do Bên B thanh toán.</li>";
            htmlContent += "<li>11. Không sử dụng chất kích thích khi lái xe, nếu vi phạm bên B sẽ chịu toàn bộ trách nhiệm trước pháp luật và sẽ chịu toàn bộ thiệt hại liên quan đến ngày nằm chờ gián đoạn kinh doanh, chi phí phát sinh nếu có.</li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>Điều V: Các thỏa thuận đặc biệt: </h2>";
            htmlContent += "<li>1. Bên B cam kết không thực hiện các hành vi:</li>";

            htmlContent += "<ul>";
            htmlContent += "<li>a. Sử dụng xe thuê vào mục đích cầm cố hay thế chấp, sử dụng xe sai mục đích. Không được lái xe ra khỏi lãnh thổ Việt Nam.</li>";
            htmlContent += "<li>b. Sử dụng xe thuê vào những mục đích phi pháp như: vận chuyển hàng hóa trái phép (ma túy, chất cấm, hàng lậu, những đối tượng bị truy nã...)</li>";
            htmlContent += "</ul>";

            htmlContent += "<li>2. Bên A có quyền:</li>";

            htmlContent += "<ul>";
            htmlContent += "<li>a. Báo cho cơ quan, gia đình Bên B, cơ quan điều tra nếu Bên B cố tình không liên lạc với Bên A</li>";
            htmlContent += "<li>b. Thanh lý tài sản thế chấp của Bên B, đền bù thiệt hại cho Bên A do bên B gây ra, nếu thiếu Bên A sẽ tiếp tục truy thu hoặc thực hiện các biện pháp theo quy định của pháp luật nhằm đảm bảo quyền lợi chính đáng của mình cho đến khi giải quyết xong thiệt hại;</li>";
            htmlContent += "<li>c. Bên A có quyền đơn phương hủy hợp đồng nếu thấy lái xe Bên B hoặc Bên B không đảm bảo kỹ thuật- chất lượng cho xe, cho an toàn giao thông;</li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>Điều VI: Cam kết chung: </h2>";

            htmlContent += "<ul>";
            htmlContent += "<li>1. Hai bên cam kết thực hiện đúng các quy định trong hợp đồng. Trong trường hợp xảy ra tranh chấp, hai bên chủ động cùng nhau thương lượng, giải quyết. Nếu không thành công thì hai bên cùng đưa vụ việc ra tòa án có thẩm quyền giải quyết.</li>";
            htmlContent += "<li>2. Hợp đồng này được lập thành 02 bản có giá trị pháp lý như nhau và có hiệu lực kể từ ngày ký.</li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>&nbsp;&nbsp;&nbsp;&nbsp;BÊN A &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
                ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "BÊN B</h3>";

            if (request.StaffSignature != null)
            {
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;<img style= 'width:100px; height:100%' src='" + request.StaffSignature + "' />";
                htmlContent += "<li> " + representer.Name + "</li>";
            }

            return htmlContent;
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }

}
