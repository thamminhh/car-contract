using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractStatistic.Sub_Model;

namespace CleanArchitecture.Application.Repository
{
    public class ReceiveContractRepository : IReceiveContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupRepository;
        private readonly FileRepository _fileRepository;
        private readonly IReceiveContractFileRepository _receiveContractFileRepository;
        private readonly ICarRepository _carRepository;
        private readonly IContractStatisticRepository _contractStatisticRepository; 

        public ReceiveContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupRepository,
            FileRepository fileRepository, IReceiveContractFileRepository receiveContractFileRepository, ICarRepository carRepository,
            IContractStatisticRepository contractStatisticRepository)
        {
            _contractContext = contractContext;
            _contractGroupRepository = contractGroupRepository;
            _fileRepository = fileRepository;
            _receiveContractFileRepository = receiveContractFileRepository;
            _carRepository = carRepository;
            _contractStatisticRepository= contractStatisticRepository;
        }

        //public ReceiveContractDataModel GetReceiveContractById(int id)
        //{
        //    ReceiveContract receiveContract = _contractContext.ReceiveContracts
        //        .Include(c => c.Receiver)
        //        .FirstOrDefault(c => c.Id == id);
        //    var host = _fileRepository.GetCurrentHost();
        //    var contractGroup = _contractContext.ContractGroups
        //        .Include(c => c.CustomerInfo)
        //        .Include(c => c.TransferContract)
        //        .FirstOrDefault(c => c.Id == receiveContract.ContractGroupId);

        //    return new ReceiveContractDataModel
        //    {
        //        Id = id,
        //        ReceiverId = receiveContract.ReceiverId,
        //        ReceiverName = receiveContract.Receiver.Name,
        //        ReceiverPhoneNumber = receiveContract.Receiver.PhoneNumber,
        //        ContractGroupId = receiveContract.ContractGroupId,
        //        CustomerName = contractGroup.CustomerInfo.CustomerName,
        //        CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
        //        CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
        //        CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
        //        CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,
        //        CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
        //        TransferContractId = contractGroup.TransferContract.Id,

        //        DateReceive = receiveContract.DateReceive,
        //        ReceiveAddress = receiveContract.ReceiveAddress,
        //        CurrentCarStateSpeedometerNumber = receiveContract.CurrentCarStateSpeedometerNumber,
        //        CurrentCarStateFuelPercent = receiveContract.CurrentCarStateFuelPercent,
        //        CurrentCarStateCurrentEtcAmount = receiveContract.CurrentCarStateCurrentEtcAmount,
        //        CurrentCarStateCarStatusDescription = receiveContract.CurrentCarStateCarStatusDescription,
        //        CurrentCarStateCarFrontImg = receiveContract.CurrentCarStateCarFrontImg,
        //        CurrentCarStateCarBackImg = receiveContract.CurrentCarStateCarBackImg,
        //        CurrentCarStateCarLeftImg = receiveContract.CurrentCarStateCarLeftImg,
        //        CurrentCarStateCarRightImg = receiveContract.CurrentCarStateCarRightImg,
        //        CurrentCarStateCarInteriorImg = receiveContract.CurrentCarStateCarInteriorImg,
        //        CurrentCarStateCarBackSeatImg = receiveContract.CurrentCarStateCarBackSeatImg,
        //        CurrentCarStateCarPhysicalDamage = receiveContract.CurrentCarStateCarPhysicalDamage,
        //        DepositItemAsset = receiveContract.DepositItemAsset,
        //        OriginalCondition = receiveContract.OriginalCondition,
        //        CurrentCarStateCarDamageDescription = receiveContract.CurrentCarStateCarDamageDescription,
        //        OrtherViolation = receiveContract.OrtherViolation,
        //        CarInsuranceMoney = receiveContract.CarInsuranceMoney,
        //        DetectedViolations = receiveContract.DetectedViolations,
        //        SpeedingViolationDescription = receiveContract.SpeedingViolationDescription,
        //        ForbiddenRoadViolationDescription = receiveContract.ForbiddenRoadViolationDescription,
        //        TrafficLightViolationDescription = receiveContract.TrafficLightViolationDescription,
        //        ExtraTime = receiveContract.ExtraTime,
        //        UnpaidTicketMoney = receiveContract.UnpaidTicketMoney,
        //        CreatedDate = receiveContract.CreatedDate,
        //        IsExported = receiveContract.IsExported,
        //        CustomerSignature = receiveContract.CustomerSignature,
        //        StaffSignature = receiveContract.StaffSignature,
        //        FilePath = host + receiveContract.FilePath,
        //        FileWithSignsPath = host + receiveContract.FileWithSignsPath,
        //        ContractStatusId = receiveContract.ContractStatusId,
        //    };
        //}

        public ReceiveContractDataModel GetReceiveContractByContractGroupId(int contractGroupId)
        {
            ReceiveContract receiveContract = _contractContext.ReceiveContracts
                .Include(c => c.Receiver)
                .FirstOrDefault(c => c.ContractGroupId == contractGroupId);
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .Include(c => c.TransferContract)
                .FirstOrDefault(c => c.Id == contractGroupId);
            var receiveContractFiles = _receiveContractFileRepository.GetReceiveContractFilesByReceiveContractId(receiveContract.Id);


            return new ReceiveContractDataModel
            {
                Id = receiveContract.Id,
                ReceiverId = receiveContract.ReceiverId,
                ReceiverName = receiveContract.Receiver.Name,
                ReceiverPhoneNumber = receiveContract.Receiver.PhoneNumber,
                ContractGroupId = receiveContract.ContractGroupId,
                CustomerName = contractGroup.CustomerInfo.CustomerName,
                CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
                TransferContractId = contractGroup.TransferContract.Id,

                DateReceive = receiveContract.DateReceive,
                ReceiveAddress = receiveContract.ReceiveAddress,
                CurrentCarStateSpeedometerNumber = receiveContract.CurrentCarStateSpeedometerNumber,
                CurrentCarStateFuelPercent = receiveContract.CurrentCarStateFuelPercent,
                CurrentCarStateCurrentEtcAmount = receiveContract.CurrentCarStateCurrentEtcAmount,
                CurrentCarStateCarStatusDescription = receiveContract.CurrentCarStateCarStatusDescription,
                OriginalCondition = receiveContract.OriginalCondition,

                DepositItemDownPayment = receiveContract.DepositItemDownPayment,
                ReturnDepostiItem = receiveContract.ReturnDepostiItem,
                CreatedDate = receiveContract.CreatedDate,
                CustomerSignature = receiveContract.CustomerSignature,
                StaffSignature = receiveContract.StaffSignature,
                FilePath = host + receiveContract.FilePath,
                FileWithSignsPath = host + receiveContract.FileWithSignsPath,
                ContractStatusId = receiveContract.ContractStatusId,

                TotalKilometersTraveled = receiveContract.TotalKilometersTraveled,
                CurrentCarStateCarDamageDescription = receiveContract.CurrentCarStateCarDamageDescription,
                InsuranceMoney = receiveContract.InsuranceMoney,
                ExtraTime = receiveContract.ExtraTime,

                DetectedViolations = receiveContract.DetectedViolations,
                SpeedingViolationDescription = receiveContract.SpeedingViolationDescription,
                ForbiddenRoadViolationDescription = receiveContract.ForbiddenRoadViolationDescription,
                TrafficLightViolationDescription = receiveContract.TrafficLightViolationDescription,
                OrtherViolation = receiveContract.OrtherViolation,
                ViolationMoney = receiveContract.ViolationMoney,
                ReceiveContractFileDataModels = receiveContractFiles,
                
            };
        }

        public bool ReceiveContractExit(int id)
        {
            return _contractContext.ReceiveContracts.Any(c => c.Id == id);
        }

        public void CreateReceiveContract(ReceiveContractCreateModel request)
        {
            //Create PDF 
            //string htmlContent = CreateReceiveContractContent(request);

            var receiveer = _contractContext.Users.FirstOrDefault(c => c.Id == request.ReceiverId);
            var transferContract = _contractContext.TransferContracts.FirstOrDefault(c => c.Id == request.TransferContractId);

            var contractGroup = _contractContext.ContractGroups
                            .Include(c => c.CustomerInfo)
                            .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarGenerallInfo)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var parkingLot = _contractContext.ParkingLots.FirstOrDefault(c => c.Id == car.ParkingLotId);
            var rentContract = _contractContext.RentContracts.Where(c => c.ContractGroupId == contractGroup.Id).OrderByDescending(c => c.Id).FirstOrDefault();
            double? totalKilometersTraveled = request.CurrentCarStateSpeedometerNumber - transferContract.CurrentCarStateSpeedometerNumber;
            double? overKm = totalKilometersTraveled - car.CarGenerallInfo.LimitedKmForMonth;
            double? extraTimeMoney = request.ExtraTime * rentContract.CarGeneralInfoAtRentPricePerHourExceed;
            //cal fuelPercent have using
            int? fuelPercent = transferContract.CurrentCarStateFuelPercent - request.CurrentCarStateFuelPercent;
            double? fuelMoney = calFuelMoney(car.TankCapacity, fuelPercent, request.CurrentFuelMoney);
            // cal etc using
            double? etcMoney = transferContract.CurrentCarStateCurrentEtcAmount - request.CurrentCarStateCurrentEtcAmount;
            //cal extraKm
            double? extraKmMoney;
            if (overKm > 0)
            {
                extraKmMoney = overKm * car.CarGenerallInfo.OverLimitedMileage;
            }
            else
            {
                overKm = 0;
                extraKmMoney = 0;
            }
            double? deposit = request.DepositItemDownPayment - etcMoney - fuelMoney - extraTimeMoney - extraKmMoney - request.InsuranceMoney - request.ViolationMoney;

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

            htmlContent += "<h2 style='text-align: center;'>BIÊN BẢN NHẬN XE</h2>";

            htmlContent += "<p>Trước và sau khi cho thuê, kèm theo hợp đồng thuê xe số " + request.ContractGroupId + "</p>";
            htmlContent += "<p>Hôm nay, ngày " + request.DateReceive + ", chúng tôi gồm:</p>";
            htmlContent += " <h2>BÊN CHO THUÊ XE (BÊN A): CÔNG TY CỔ PHẦN CÔNG NGHỆ ATSHARE</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ: " + parkingLot.Address + "</li>";
            htmlContent += "<li>MST:0110032942</li> ";
            htmlContent += "<li>STK:19035651301016 tại NH Kỹ Thương VN (Techcombank), CN Hai Bà Trưng</li>";
            htmlContent += "<li>Đại diện: " + receiveer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + receiveer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B): " + contractGroup.CustomerInfo.CustomerName + "</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>Ngày cấp: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>Địa chỉ: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
            htmlContent += "<li>Điện thoại người thân:(chỉ dùng trong trường hợp khẩn khi không liên lạc được với bên B): " + contractGroup.CustomerInfo.RelativeTel + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>A. Tình trạng xe khi nhận lại xe </h3>";
            htmlContent += "<p>Tình trạng nội thất, ngoại thất và máy móc xe: </p>";
            htmlContent += "<ul>";
            if (request.OriginalCondition == true)
            {
                htmlContent += "<li>Giống tình trạng ban đầu (nội thất, ngoại thất, máy móc, giấy tờ, đồ dự phòng).</li>";
            }
            if (request.OriginalCondition == false)
            {
                htmlContent += "<li>Khác tình trạng ban đầu, các hư hỏng và mất mát sau: " + request.CurrentCarStateCarDamageDescription + "</li>";
                htmlContent += "<li>Chi phí khắc phục: đang cập nhật </li>";
            }
            htmlContent += "</ul>";

            htmlContent += "<ul>";
            htmlContent += "<li>Số công tơ mét: " + request.CurrentCarStateSpeedometerNumber + " Km</li>";
            htmlContent += "<li>Tổng số km đã đi: " + totalKilometersTraveled + " Km</li>";
            htmlContent += "<li>Nằm trong giới hạn km: " + car.CarGenerallInfo.LimitedKmForMonth + " Km</li>";
            if (overKm > 0)
            {
                htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
                htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
            }
            else
            {
                overKm = 0;
                htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
                htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
            }
            htmlContent += "<li>Đồng hồ xăng/dầu: " + request.CurrentCarStateFuelPercent + " %</li>";
            htmlContent += "<li>Thời gian phụ trội so với hợp đồng: " + request.ExtraTime + " giờ, phụ phí phát sinh: " + extraTimeMoney + " VNĐ</li>";
            if (etcMoney > 0)
            {
                htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
            }
            else
            {
                htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
            }
            htmlContent += "</ul>";

            htmlContent += "<h3>B. Tình trạng pháp lí: </h3>";
            if (request.DetectedViolations == true)
            {
                htmlContent += "<p>Các lỗi ghi nhận được trong quá trình thuê: </p>";
                htmlContent += "<ul>";
                htmlContent += "<li>Vượt tốc độ: " + request.SpeedingViolationDescription + "</li>";
                htmlContent += "<li>Vào đường cấm: " + request.ForbiddenRoadViolationDescription + "</li>";
                htmlContent += "<li>Vượt đèn đỏ: " + request.TrafficLightViolationDescription + "</li>";
                htmlContent += "<li>Các lỗi khác(nếu có): " + request.OrtherViolation + "</li>";
                htmlContent += "<li>Tổng tiền vi phạm: " + request.ViolationMoney + " VNĐ</li>";
                htmlContent += "</ul>";
            }
            else
            {
                htmlContent += "<p>Chưa phát hiện lỗi gì</p>";
            }
            //double? totalCostsIsncurred = request.CarInsuranceMoney + overKm * car.CarGenerallInfo.OverLimitedMileage + extraTimeMoney + request.UnpaidTicketMoney;
            //htmlContent += "<p>Tổng chi phí phát sinh so với hợp đồng: " + totalCostsIsncurred + "</p>";
            htmlContent += "<h3>C. Tổng chi phí phát sinh so với hợp đồng: </h3>";
            htmlContent += "<ul>";
            if (request.ReturnDepostiItem == true)
            {
                htmlContent += "<li>Số tiền đặt cọc đã thu: " + request.DepositItemDownPayment + " VNĐ</li>";

                htmlContent += "<li>Số tiền phải trả: " + request.DepositItemDownPayment + " VNĐ</li>";
            }
            else
            {
                htmlContent += "<li>Số tiền đặt cọc đã thu: " + request.DepositItemDownPayment + " VNĐ</li>";
                if (etcMoney > 0)
                {
                    htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
                }
                else
                {
                    htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
                }
                htmlContent += "<li>Tiền xăng đã sử dụng: " + fuelMoney + " VNĐ</li>";
                htmlContent += "<li>Thời gian phụ trội so với hợp đồng: " + request.ExtraTime + " giờ, phụ phí phát sinh: " + extraTimeMoney + " VNĐ</li>";
                if (overKm > 0)
                {
                    htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
                }
                else
                {
                    overKm = 0;
                    htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
                }

                htmlContent += "<li>Chi phí bảo hiểm: " + request.InsuranceMoney + "  VNĐ</li>";
                htmlContent += "<li>Tổng tiền vi phạm: " + request.ViolationMoney + " VNĐ</li>";

                if(deposit >= 0)
                {
                    htmlContent += "<li>Số tiền cọc trả lại cho khách: " + deposit + " VNĐ</li>";
                }
                if(deposit < 0)
                {
                    htmlContent += "<li>Số tiền phải thu thêm từ khách: " + deposit * (-1) + " VNĐ</li>";
                }

            }

            htmlContent += "<li>Cam kết của Khách thuê và Chủ xe: </li>";
            htmlContent += "<li>Trong trường hợp phát sinh các khoản phạt nguội, bằng chứng do camera giám sát của Cục CSGT - Bộ Công An " +
                "ghi nhận được trong thời gian Bên B sử dụng xe ô tô thuê của Bên A.</li>";
            htmlContent += "<li>Bên A có trách nhiệm cung cấp các bằng chứng liên quan cho bên B ngay khi nhận được thông tin.</li>";
            htmlContent += "<li>Bên B cam kết chịu hoàn toàn trách nhiệm và bồi thường toàn bộ các chi phí liên quan cho Bên A. </li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>&nbsp;&nbsp;&nbsp;&nbsp;BÊN A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
                ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "BÊN B</h3>";

            var file = _fileRepository.GeneratePdfAsync(htmlContent);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());
            //Create ReceiveContract
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
                DepositItemDownPayment = request.DepositItemDownPayment,
                ReturnDepostiItem = request.ReturnDepostiItem,
                OriginalCondition = request.OriginalCondition,
                CurrentCarStateCarDamageDescription = request.CurrentCarStateCarDamageDescription,
                DetectedViolations = request.DetectedViolations,
                SpeedingViolationDescription = request.SpeedingViolationDescription,
                ForbiddenRoadViolationDescription = request.ForbiddenRoadViolationDescription,
                TrafficLightViolationDescription = request.TrafficLightViolationDescription,
                OrtherViolation = request.OrtherViolation,
                TotalKilometersTraveled = totalKilometersTraveled,
                ExtraTime = request.ExtraTime,
                CreatedDate = request.CreatedDate,
                ContractStatusId = defaultContractId,
                FilePath = filePath,
                ViolationMoney = request.ViolationMoney,
                InsuranceMoney = request.InsuranceMoney,
                
            };
            _contractContext.ReceiveContracts.Add(receiveContract);
            _contractContext.SaveChanges();

            //Create list ReceiveContractFile
            var receiveContractFiles = request.ReceiveContractFileCreateModels;
            List<ReceiveContractFile> receiveContractFileList = new List<ReceiveContractFile>();
            if (receiveContractFiles != null && receiveContractFiles.Any())
            {
                foreach (var receiveContractFileCreate in receiveContractFiles)
                {
                    var receiveFile = new ReceiveContractFile
                    {
                        ReceiveContractId = receiveContract.Id,
                        Title = receiveContractFileCreate.Title,
                        DocumentImg = receiveContractFileCreate.DocumentImg,
                        DocumentDescription = receiveContractFileCreate.DocumentDescription,
                    };
                    receiveContractFileList.Add(receiveFile);
                }
                _contractContext.ReceiveContractFiles.AddRange(receiveContractFileList);
                _contractContext.SaveChanges();
            }

            if (request.OriginalCondition == false || request.ReturnDepostiItem == false)
            {
                var contractGroupStatusInspecting = Constant.ContractGroupConstant.ContractInspecting;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusInspecting;
                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

            }
            if(request.OriginalCondition == true && request.ReturnDepostiItem == true)
            {
                //Update ContractGroupStatus
                var contractGroupStatusReceiveNotSign = Constant.ContractGroupConstant.ReceiveContractNotSign;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusReceiveNotSign;
                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
            }

    }

        public void UpdateReceiveContract(int id, ReceiveContractUpdateModel request)
        {

            var receiveer = _contractContext.Users.FirstOrDefault(c => c.Id == request.ReceiverId);
            var transferContract = _contractContext.TransferContracts.FirstOrDefault(c => c.Id == request.TransferContractId);

            var contractGroup = _contractContext.ContractGroups
                            .Include(c => c.CustomerInfo)
                            .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarGenerallInfo)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            var parkingLot = _contractContext.ParkingLots.FirstOrDefault(c => c.Id == car.ParkingLotId);
            var rentContract = _contractContext.RentContracts.Where(c => c.ContractGroupId == contractGroup.Id).OrderByDescending(c => c.Id).FirstOrDefault();
            double? totalKilometersTraveled = request.CurrentCarStateSpeedometerNumber - transferContract.CurrentCarStateSpeedometerNumber;
            double? overKm = totalKilometersTraveled - car.CarGenerallInfo.LimitedKmForMonth;
            double? extraTimeMoney = request.ExtraTime * rentContract.CarGeneralInfoAtRentPricePerHourExceed;
            //cal fuelPercent have using
            int? fuelPercent = transferContract.CurrentCarStateFuelPercent - request.CurrentCarStateFuelPercent;
            double? fuelMoney = calFuelMoney(car.TankCapacity, fuelPercent, request.CurrentFuelMoney);
            // cal etc using
            double? etcMoney = transferContract.CurrentCarStateCurrentEtcAmount - request.CurrentCarStateCurrentEtcAmount;
            //cal extraKm
            double? extraKmMoney;
            if (overKm > 0)
            {
                extraKmMoney = overKm * car.CarGenerallInfo.OverLimitedMileage;
            }
            else
            {
                overKm = 0;
                extraKmMoney = 0;
            }
            double? deposit = request.DepositItemDownPayment - etcMoney - fuelMoney - extraTimeMoney - extraKmMoney - request.InsuranceMoney - request.ViolationMoney;

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

            htmlContent += "<h2 style='text-align: center;'>BIÊN BẢN NHẬN XE</h2>";

            htmlContent += "<p>Trước và sau khi cho thuê, kèm theo hợp đồng thuê xe số " + request.ContractGroupId + "</p>";
            htmlContent += "<p>Hôm nay, ngày " + request.DateReceive + ", chúng tôi gồm:</p>";
            htmlContent += " <h2>BÊN CHO THUÊ XE (BÊN A): CÔNG TY CỔ PHẦN CÔNG NGHỆ ATSHARE</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ: " + parkingLot.Address + "</li>";
            htmlContent += "<li>MST:0110032942</li> ";
            htmlContent += "<li>STK:19035651301016 tại NH Kỹ Thương VN (Techcombank), CN Hai Bà Trưng</li>";
            htmlContent += "<li>Đại diện: " + receiveer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + receiveer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B): " + contractGroup.CustomerInfo.CustomerName + "</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>Ngày cấp: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>Địa chỉ: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
            htmlContent += "<li>Điện thoại người thân:(chỉ dùng trong trường hợp khẩn khi không liên lạc được với bên B): " + contractGroup.CustomerInfo.RelativeTel + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>A. Tình trạng xe khi nhận lại xe </h3>";
            htmlContent += "<p>Tình trạng nội thất, ngoại thất và máy móc xe: </p>";
            htmlContent += "<ul>";
            if (request.OriginalCondition == true)
            {
                htmlContent += "<li>Giống tình trạng ban đầu (nội thất, ngoại thất, máy móc, giấy tờ, đồ dự phòng).</li>";
            }
            if (request.OriginalCondition == false)
            {
                htmlContent += "<li>Khác tình trạng ban đầu, các hư hỏng và mất mát sau: " + request.CurrentCarStateCarDamageDescription + "</li>";
                htmlContent += "<li>Chi phí khắc phục: đang cập nhật </li>";
            }
            htmlContent += "</ul>";

            htmlContent += "<ul>";
            htmlContent += "<li>Số công tơ mét: " + request.CurrentCarStateSpeedometerNumber + " Km</li>";
            htmlContent += "<li>Tổng số km đã đi: " + totalKilometersTraveled + " Km</li>";
            htmlContent += "<li>Nằm trong giới hạn km: " + car.CarGenerallInfo.LimitedKmForMonth + " Km</li>";
            if (overKm > 0)
            {
                htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
                htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
            }
            else
            {
                overKm = 0;
                htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
                htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
            }
            htmlContent += "<li>Đồng hồ xăng/dầu: " + request.CurrentCarStateFuelPercent + " %</li>";
            htmlContent += "<li>Thời gian phụ trội so với hợp đồng: " + request.ExtraTime + " giờ, phụ phí phát sinh: " + extraTimeMoney + " VNĐ</li>";
            if (etcMoney > 0)
            {
                htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
            }
            else
            {
                htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
            }
            htmlContent += "</ul>";

            htmlContent += "<h3>B. Tình trạng pháp lí: </h3>";
            if (request.DetectedViolations == true)
            {
                htmlContent += "<p>Các lỗi ghi nhận được trong quá trình thuê: </p>";
                htmlContent += "<ul>";
                htmlContent += "<li>Vượt tốc độ: " + request.SpeedingViolationDescription + "</li>";
                htmlContent += "<li>Vào đường cấm: " + request.ForbiddenRoadViolationDescription + "</li>";
                htmlContent += "<li>Vượt đèn đỏ: " + request.TrafficLightViolationDescription + "</li>";
                htmlContent += "<li>Các lỗi khác(nếu có): " + request.OrtherViolation + "</li>";
                htmlContent += "<li>Tổng tiền vi phạm: " + request.ViolationMoney + " VNĐ</li>";
                htmlContent += "</ul>";
            }
            else
            {
                htmlContent += "<p>Chưa phát hiện lỗi gì</p>";
            }
            //double? totalCostsIsncurred = request.CarInsuranceMoney + overKm * car.CarGenerallInfo.OverLimitedMileage + extraTimeMoney + request.UnpaidTicketMoney;
            //htmlContent += "<p>Tổng chi phí phát sinh so với hợp đồng: " + totalCostsIsncurred + "</p>";
            htmlContent += "<h3>C. Tổng chi phí phát sinh so với hợp đồng: </h3>";
            htmlContent += "<ul>";
            if (request.ReturnDepostiItem == true)
            {
                htmlContent += "<li>Số tiền đặt cọc đã thu: " + request.DepositItemDownPayment + " VNĐ</li>";

                htmlContent += "<li>Số tiền phải trả: " + request.DepositItemDownPayment + " VNĐ</li>";
            }
            else
            {
                htmlContent += "<li>Số tiền đặt cọc đã thu: " + request.DepositItemDownPayment + " VNĐ</li>";
                if (etcMoney > 0)
                {
                    htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
                }
                else
                {
                    htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + etcMoney + " VNĐ</li>";
                }
                htmlContent += "<li>Tiền xăng đã sử dụng: " + fuelMoney + " VNĐ</li>";
                htmlContent += "<li>Thời gian phụ trội so với hợp đồng: " + request.ExtraTime + " giờ, phụ phí phát sinh: " + extraTimeMoney + " VNĐ</li>";
                if (overKm > 0)
                {
                    htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
                }
                else
                {
                    overKm = 0;
                    htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
                }
                    htmlContent += "<li>Chi phí bảo hiểm: " + request.InsuranceMoney + "  VNĐ</li>";
                
                    htmlContent += "<li>Tổng tiền vi phạm: " + request.ViolationMoney + " VNĐ</li>";
                
                if (deposit >= 0)
                {
                    htmlContent += "<li>Số tiền cọc trả lại cho khách: " + deposit + " VNĐ</li>";
                }
                if (deposit < 0)
                {
                    htmlContent += "<li>Số tiền phải thu thêm từ khách: " + deposit * (-1) + " VNĐ</li>";
                }

            }

            htmlContent += "<li>Cam kết của Khách thuê và Chủ xe: </li>";
            htmlContent += "<li>Trong trường hợp phát sinh các khoản phạt nguội, bằng chứng do camera giám sát của Cục CSGT - Bộ Công An " +
                "ghi nhận được trong thời gian Bên B sử dụng xe ô tô thuê của Bên A.</li>";
            htmlContent += "<li>Bên A có trách nhiệm cung cấp các bằng chứng liên quan cho bên B ngay khi nhận được thông tin.</li>";
            htmlContent += "<li>Bên B cam kết chịu hoàn toàn trách nhiệm và bồi thường toàn bộ các chi phí liên quan cho Bên A. </li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>&nbsp;&nbsp;&nbsp;&nbsp;BÊN A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
                ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "BÊN B</h3>";

            if (request.StaffSignature != null)
            {
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;<img style= 'width:100px; height:100%' src='" + request.StaffSignature + "' />";
            }
            if (request.CustomerSignature != null)
            {
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
              "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
              "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
              ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
              "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;<img style= 'width:100px; height:100%' src='" + request.CustomerSignature + "' />";
            }
            var file = _fileRepository.GeneratePdfAsync(htmlContent);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());

          
            //UpdateContract
            var receiveContractFiles = request.ReceiveContractFileDataModels;

            var receiveContract = _contractContext.ReceiveContracts.Find(id);

            receiveContract.ContractGroupId = request.ContractGroupId;
            receiveContract.ReceiverId = request.ReceiverId;
            receiveContract.DateReceive = request.DateReceive;
            receiveContract.ReceiveAddress = request.ReceiveAddress;

            receiveContract.CurrentCarStateSpeedometerNumber = request.CurrentCarStateSpeedometerNumber;
            receiveContract.CurrentCarStateFuelPercent = request.CurrentCarStateFuelPercent;
            receiveContract.CurrentCarStateCurrentEtcAmount = request.CurrentCarStateCurrentEtcAmount;
            receiveContract.CurrentCarStateCarStatusDescription = request.CurrentCarStateCarStatusDescription;

            receiveContract.OriginalCondition = request.OriginalCondition;
            receiveContract.DepositItemDownPayment = request.DepositItemDownPayment;
            receiveContract.ReturnDepostiItem = request.ReturnDepostiItem;
            receiveContract.CustomerSignature = request.CustomerSignature;
            receiveContract.StaffSignature = request.StaffSignature;
            receiveContract.FileWithSignsPath = filePath;
            receiveContract.ContractStatusId = request.ContractStatusId;
            if (request.CustomerSignature != null && request.StaffSignature != null)
            {
                receiveContract.ContractStatusId = Constant.ContractStatusConstant.ContractExported;
            }
            else
            {
                receiveContract.ContractStatusId = request.ContractStatusId;
            }
            if (receiveContractFiles != null)
            {
                foreach (var receiveContractFile in receiveContractFiles)
                {
                    var existingFile = _contractContext.ReceiveContractFiles.FirstOrDefault(cf => cf.Id == receiveContractFile.Id && cf.ReceiveContractId == receiveContract.Id);
                    if (existingFile != null)
                    {
                        existingFile.Title = receiveContractFile.Title ?? existingFile.Title;
                        existingFile.DocumentImg = receiveContractFile.DocumentImg ?? existingFile.DocumentImg;
                        existingFile.DocumentDescription = receiveContractFile.DocumentDescription ?? existingFile.DocumentDescription;
                    }
                    else
                    {
                        _contractContext.ReceiveContractFiles.Add(new ReceiveContractFile
                        {
                            ReceiveContractId = receiveContract.Id,
                            Title = receiveContractFile.Title,
                            DocumentImg = receiveContractFile.DocumentImg,
                            DocumentDescription = receiveContractFile.DocumentDescription
                        });
                    }
                }
            }
            receiveContract.TotalKilometersTraveled = totalKilometersTraveled;
            receiveContract.CurrentCarStateCarDamageDescription = request.CurrentCarStateCarDamageDescription;
            receiveContract.InsuranceMoney = request.InsuranceMoney;
            receiveContract.ExtraTime = request.ExtraTime;
            receiveContract.DetectedViolations = request.DetectedViolations;
            receiveContract.SpeedingViolationDescription = request.SpeedingViolationDescription;
            receiveContract.ForbiddenRoadViolationDescription = request.ForbiddenRoadViolationDescription;
            receiveContract.TrafficLightViolationDescription = request.TrafficLightViolationDescription;
            receiveContract.OrtherViolation = request.OrtherViolation;
            receiveContract.ViolationMoney = request.ViolationMoney;

            _contractContext.ReceiveContracts.Update(receiveContract);
            _contractContext.SaveChanges();
            if (request.CustomerSignature == null)
            {
                if (request.OriginalCondition == true && request.ReturnDepostiItem == false)
                {
                    var contractGroupStatusInspecting = Constant.ContractGroupConstant.ContractInspecting;
                    var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                    contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                    contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusInspecting;
                    _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

                }

                if (request.OriginalCondition == false && request.ReturnDepostiItem == false)
                {
                    var contractGroupStatusInspecting = Constant.ContractGroupConstant.ContractInspecting;
                    var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                    contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                    contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusInspecting;
                    _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

                }

                if (request.OriginalCondition == true && request.ReturnDepostiItem == true)
                {
                    //Update ContractGroupStatus
                    var contractGroupStatusReceiveNotSign = Constant.ContractGroupConstant.ReceiveContractNotSign;
                    var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                    contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                    contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusReceiveNotSign;
                    _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
                }
            }

            if (request.CustomerSignature != null && request.StaffSignature != null)
            {
                //Update ContractGroupStatus
                var contractGroupStatusReceiveSigned = Constant.ContractGroupConstant.ReceiveContractSigned;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusReceiveSigned;
                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

                //Update CarStatus 
                var carAvailableStatus = Constant.CarStatusConstants.Available;
                var carStatusUpdateModel = new CarUpdateStatusModel();
                carStatusUpdateModel.id = (int)contractGroup.CarId;
                carStatusUpdateModel.CarStatusId = carAvailableStatus;
                _carRepository.UpdateCarStatus((int)contractGroup.CarId, carStatusUpdateModel);
                _contractContext.SaveChanges();

                //Update CarState
                var carState = _contractContext.CarStates.Where(c => c.CarId == contractGroup.CarId).FirstOrDefault();
                carState.CurrentEtcAmount = request.CurrentCarStateCurrentEtcAmount;
                carState.FuelPercent = request.CurrentCarStateFuelPercent;
                carState.SpeedometerNumber = request.CurrentCarStateSpeedometerNumber;
                _contractContext.CarStates.Update(carState);
                _contractContext.SaveChanges();

                //Update CarMaintainanceInfo
                var carMaintenanceInfo = _contractContext.CarMaintenanceInfos
                    .Where(c => c.CarId == contractGroup.CarId)
                    .OrderByDescending(c => c.Id)
                    .FirstOrDefault();

                carMaintenanceInfo.KmTraveled = carMaintenanceInfo.KmTraveled + totalKilometersTraveled;
                _contractContext.CarMaintenanceInfos.Update(carMaintenanceInfo);
                _contractContext.SaveChanges();


                //Update ContractStatistic
                var contractStatistic = _contractContext.ContractStatistics
                    .Where(c => c.ContractGroupId == contractGroup.Id).FirstOrDefault();

                var contractStatisticUpdateModel = new ContractStatisticUpdateModel();
                contractStatisticUpdateModel.Id = contractStatistic.Id;
                contractStatisticUpdateModel.ContractGroupId = contractStatistic.ContractGroupId;
                contractStatisticUpdateModel.FuelMoneyUsing = calFuelMoney(car.TankCapacity, fuelPercent, request.CurrentFuelMoney);
                contractStatisticUpdateModel.EtcmoneyUsing = etcMoney;
                contractStatisticUpdateModel.ExtraTimeMoney = request.ExtraTime * rentContract.CarGeneralInfoAtRentPricePerHourExceed;
                contractStatisticUpdateModel.ExtraKmMoney = extraKmMoney;
                contractStatisticUpdateModel.InsuranceMoney = request.InsuranceMoney;
                contractStatisticUpdateModel.ViolationMoney = request.ViolationMoney;
                contractStatisticUpdateModel.PaymentAmount = contractStatistic.PaymentAmount;
                _contractStatisticRepository.UpdateContractStatistic(contractStatisticUpdateModel);
            }
        }
        private double? calFuelMoney(decimal? tankCapacity, int? fuelPercent, double? currentFuelMoney)
        {
            double? carFuelMoney;
            double? dTankCapacity = Decimal.ToDouble((decimal)tankCapacity);
            return carFuelMoney = (dTankCapacity * fuelPercent/100) * currentFuelMoney;
        }

        public bool UpdateReceiveContractStatus(int id, ReceiveContractUpdateStatusModel request)
        {
            var ReceiveContract = _contractContext.ReceiveContracts.Where(c => c.Id == id).FirstOrDefault();

            if (ReceiveContract == null)
                return false;

            ReceiveContract.ContractStatusId = request.ContractStatusId;
            return Save();
        }
        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public string CreateReceiveContractContent(ReceiveContractCreateModel request)
        {
            var receiveer = _contractContext.Users.FirstOrDefault(c => c.Id == request.ReceiverId);
            var transferContract = _contractContext.TransferContracts.FirstOrDefault(c => c.Id == request.TransferContractId);

            var contractGroup = _contractContext.ContractGroups
                            .Include(c => c.CustomerInfo)
                            .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarGenerallInfo)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var rentContract = _contractContext.RentContracts.FirstOrDefault(c => c.ContractGroupId == request.ContractGroupId);
            double? totalKilometersTraveled = request.CurrentCarStateSpeedometerNumber - transferContract.CurrentCarStateSpeedometerNumber;
            double? overKm = totalKilometersTraveled - car.CarGenerallInfo.LimitedKmForMonth;
            double? extraTimeMoney = request.ExtraTime * rentContract.CarGeneralInfoAtRentPricePerHourExceed;

            string htmlContent = "<h1 style='text-align:center;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style='text-align:center;'>Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h2>BIÊN BẢN NHẬN XE </h2>";

            htmlContent += "<p>Hôm nay, vào hồi " + request.DateReceive + ", chúng tôi gồm:</p>";
            htmlContent += "<h2>BÊN CHO THUÊ XE (BÊN A): </h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Đại diện: " + receiveer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + receiveer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B):</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Họ tên: " + contractGroup.CustomerInfo.CustomerName + "</li>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>Ngày cấp: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>Địa chỉ: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
            htmlContent += "<li>Điện thoại người thân:(chỉ dùng trong trường hợp khẩn khi không liên lạc được với bên B): " + contractGroup.CustomerInfo.RelativeTel + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Tình trạng xe khi nhận lại xe </p>";
            htmlContent += "<p>Tình trạng nội thất, ngoại thất và máy móc xe: </p>";
            htmlContent += "<ul>";
            if (request.OriginalCondition == true)
            {
                htmlContent += "<li>Giống tình trạng ban đầu (nội thất, ngoại thất, máy móc, giấy tờ, đồ dự phòng).</li>";
            }
            if (request.OriginalCondition == false)
            {
                htmlContent += "<liKhác tình trạng ban đầu, các hư hỏng và mất mát sau: " + request.CurrentCarStateCarDamageDescription + "</li>";
                //htmlContent += "<li>Chi phí khắc phục (tạm tính): " + request.CarInsuranceMoney + "</li>";
            }
            htmlContent += "</ul>";

            htmlContent += "<ul>";
            htmlContent += "<li>Số công tơ mét: " + request.CurrentCarStateSpeedometerNumber + " Km</li>";
            //htmlContent += "<li>Tổng số km đã đi: " + totalKilometersTraveled + " Km</li>";
            htmlContent += "<li>Nằm trong giới hạn km: " + car.CarGenerallInfo.LimitedKmForMonth + " Km</li>";
            if (overKm > 0)
            {
                htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
                htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";

            }
            else
            {
                overKm = 0;
                htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
                htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
            }
            htmlContent += "<li>Đồng hồ xăng/dầu: " + request.CurrentCarStateFuelPercent + " %</li>";
            htmlContent += "<li>Thời gian phụ trội so với hợp đồng: " + request.ExtraTime + " giờ, phụ phí phát sinh: " + extraTimeMoney + " VNĐ</li>";
            //htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + request.UnpaidTicketMoney + "</li>";
            htmlContent += "</ul>";

            if (request.DetectedViolations == true)
            {
                htmlContent += "<p>Các lỗi ghi nhận được trong quá trình thuê: </p>";
                htmlContent += "<ul>";
                htmlContent += "<li>Vượt tốc độ: " + request.SpeedingViolationDescription + "</li>";
                htmlContent += "<li>Vào đường cấm: " + request.ForbiddenRoadViolationDescription + "</li>";
                htmlContent += "<li>Vượt đèn đỏ: " + request.TrafficLightViolationDescription + "</li>";
                htmlContent += "<li>Các lỗi khác(nếu có): " + request.OrtherViolation + "</li>";
                htmlContent += "</ul>";
            }
            else
            {
                htmlContent += "<p>Chưa phát hiện lỗi gì</p>";
            }
            //double? totalCostsIsncurred = request.CarInsuranceMoney + overKm * car.CarGenerallInfo.OverLimitedMileage + extraTimeMoney + request.UnpaidTicketMoney;
            //htmlContent += "<p>Tổng chi phí phát sinh so với hợp đồng: " + totalCostsIsncurred + "</p>";

            htmlContent += "<ul>";
            htmlContent += "<li>Bên A đã hoàn trả cho bên B một số giấy tờ và tài sản như sau: </li>";
            htmlContent += "<li>Toàn bộ giấy tờ và tài sản tại thời điểm giao nhận: </li>";
            htmlContent += "<li>Thiếu hoặc chưa hoàn trả các giấy tờ và tài sản sau: </li>";
            htmlContent += "<li>Cam kết của Khách thuê và Chủ xe: </li>";
            htmlContent += "<li>Trong trường hợp phát sinh các khoản phạt nguội, bằng chứng do camera giám sát của Cục CSGT - Bộ Công An " +
                "ghi nhận được trong thời gian Bên B sử dụng xe ô tô thuê của Bên A.</li>";
            htmlContent += "<li>Bên A có trách nhiệm cung cấp các bằng chứng liên quan cho bên B ngay khi nhận được thông tin.</li>";
            htmlContent += "<li>Bên B cam kết chịu hoàn toàn trách nhiệm và bồi thường toàn bộ các chi phí liên quan cho Bên A. </li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>&nbsp;&nbsp;&nbsp;&nbsp;BÊN A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
                ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "BÊN B</h3>";

            return htmlContent;
        }
        public string UpdateReceiveContractContent(ReceiveContractUpdateModel request)
        {
            var receiveer = _contractContext.Users.FirstOrDefault(c => c.Id == request.ReceiverId);
            //var transferContract = _contractContext.TransferContracts.FirstOrDefault(c => c.Id == request.TransferContractId);

            var contractGroup = _contractContext.ContractGroups
                            .Include(c => c.CustomerInfo)
                            .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .Include(c => c.CarGenerallInfo)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var rentContract = _contractContext.RentContracts.FirstOrDefault(c => c.ContractGroupId == request.ContractGroupId);
            //double? totalKilometersTraveled = request.CurrentCarStateSpeedometerNumber - transferContract.CurrentCarStateSpeedometerNumber;
            //double? overKm = totalKilometersTraveled - car.CarGenerallInfo.LimitedKmForMonth;
            double? extraTimeMoney = request.ExtraTime * rentContract.CarGeneralInfoAtRentPricePerHourExceed;

            string htmlContent = "<h1 style='text-align:center;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style='text-align:center;'>Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h2>BIÊN BẢN NHẬN XE </h2>";

            htmlContent += "<p>Hôm nay, vào hồi " + request.DateReceive + ", chúng tôi gồm:</p>";
            htmlContent += "<h2>BÊN CHO THUÊ XE (BÊN A): </h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Đại diện: " + receiveer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + receiveer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B):</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Họ tên: " + contractGroup.CustomerInfo.CustomerName + "</li>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>Ngày cấp: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>Địa chỉ: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
            htmlContent += "<li>Điện thoại người thân:(chỉ dùng trong trường hợp khẩn khi không liên lạc được với bên B): " + contractGroup.CustomerInfo.RelativeTel + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Tình trạng xe khi nhận lại xe </p>";
            htmlContent += "<p>Tình trạng nội thất, ngoại thất và máy móc xe: </p>";
            htmlContent += "<ul>";
            if (request.OriginalCondition == true)
            {
                htmlContent += "<li>Giống tình trạng ban đầu (nội thất, ngoại thất, máy móc, giấy tờ, đồ dự phòng).</li>";
            }
            if (request.OriginalCondition == false)
            {
                htmlContent += "<liKhác tình trạng ban đầu, các hư hỏng và mất mát sau: " + request.CurrentCarStateCarDamageDescription + "</li>";
                //htmlContent += "<li>Chi phí khắc phục (tạm tính): " + request.CarInsuranceMoney + "</li>";
            }
            htmlContent += "</ul>";

            htmlContent += "<ul>";
            htmlContent += "<li>Số công tơ mét: " + request.CurrentCarStateSpeedometerNumber + " Km</li>";
            //htmlContent += "<li>Tổng số km đã đi: " + totalKilometersTraveled + " Km</li>";
            //htmlContent += "<li>Nằm trong giới hạn km: " + car.CarGenerallInfo.LimitedKmForMonth + " Km</li>";
            //if (overKm > 0)
            //{
            //    htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
            //    htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";

            //}
            //else
            //{
            //    overKm = 0;
            //    htmlContent += "<li>Vượt số giới hạn km, số km vượt: " + overKm + " Km</li>";
            //    htmlContent += "<li>Số tiền phụ trội km: " + overKm * car.CarGenerallInfo.OverLimitedMileage + " VNĐ</li>";
            //}
            //htmlContent += "<li>Đồng hồ xăng/dầu: " + request.CurrentCarStateFuelPercent + " %</li>";
            htmlContent += "<li>Thời gian phụ trội so với hợp đồng: " + request.ExtraTime + " giờ, phụ phí phát sinh: " + extraTimeMoney + " VNĐ</li>";
            //htmlContent += "<li>Vé cầu đường phát sinh chưa thanh toán: " + request.UnpaidTicketMoney + "</li>";
            htmlContent += "</ul>";

            if (request.DetectedViolations == true)
            {
                htmlContent += "<p>Các lỗi ghi nhận được trong quá trình thuê: </p>";
                htmlContent += "<ul>";
                htmlContent += "<li>Vượt tốc độ: " + request.SpeedingViolationDescription + "</li>";
                htmlContent += "<li>Vào đường cấm: " + request.ForbiddenRoadViolationDescription + "</li>";
                htmlContent += "<li>Vượt đèn đỏ: " + request.TrafficLightViolationDescription + "</li>";
                htmlContent += "<li>Các lỗi khác(nếu có): " + request.OrtherViolation + "</li>";
                htmlContent += "</ul>";
            }
            else
            {
                htmlContent += "<p>Chưa phát hiện lỗi gì</p>";
            }
            //double? totalCostsIsncurred = request.CarInsuranceMoney + overKm * car.CarGenerallInfo.OverLimitedMileage + extraTimeMoney + request.UnpaidTicketMoney;
            //htmlContent += "<p>Tổng chi phí phát sinh so với hợp đồng: " + totalCostsIsncurred + "</p>";

            htmlContent += "<ul>";
            htmlContent += "<li>Bên A đã hoàn trả cho bên B một số giấy tờ và tài sản như sau: </li>";
            htmlContent += "<li>Toàn bộ giấy tờ và tài sản tại thời điểm giao nhận: </li>";
            htmlContent += "<li>Thiếu hoặc chưa hoàn trả các giấy tờ và tài sản sau: </li>";
            htmlContent += "<li>Cam kết của Khách thuê và Chủ xe: </li>";
            htmlContent += "<li>Trong trường hợp phát sinh các khoản phạt nguội, bằng chứng do camera giám sát của Cục CSGT - Bộ Công An " +
                "ghi nhận được trong thời gian Bên B sử dụng xe ô tô thuê của Bên A.</li>";
            htmlContent += "<li>Bên A có trách nhiệm cung cấp các bằng chứng liên quan cho bên B ngay khi nhận được thông tin.</li>";
            htmlContent += "<li>Bên B cam kết chịu hoàn toàn trách nhiệm và bồi thường toàn bộ các chi phí liên quan cho Bên A. </li>";
            htmlContent += "</ul>";

            htmlContent += "<h3>&nbsp;&nbsp;&nbsp;&nbsp;BÊN A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
                ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "BÊN B</h3>";

            if (request.StaffSignature != null)
            {
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;<img style= 'width:100px; height:100%' src='" + request.StaffSignature + "' />";
            }
            if (request.CustomerSignature != null)
            {
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
              "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
              "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" +
              ";&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
              "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;<img style= 'width:100px; height:100%' src='" + request.CustomerSignature + "' />";
            }

            return htmlContent;
        }
    }
}
