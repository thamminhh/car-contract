using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Repository
{
    public class ReceiveContractRepository : IReceiveContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupRepository;
        private readonly FileRepository _fileRepository;

        public ReceiveContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupRepository,
            FileRepository fileRepository)
        {
            _contractContext = contractContext;
            _contractGroupRepository = contractGroupRepository;
            _fileRepository = fileRepository;
        }

        public ReceiveContractDataModel GetReceiveContractById(int id)
        {
            ReceiveContract receiveContract = _contractContext.ReceiveContracts
                .Include(c => c.Receiver)
                .FirstOrDefault(c => c.Id == id);
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .Include(c => c.RentContract)
                .FirstOrDefault(c => c.Id == receiveContract.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            return new ReceiveContractDataModel
            {
                Id = id,
                ContractGroupId = receiveContract.ContractGroupId,
                ReceiverId = receiveContract.ReceiverId,
                ReceiverName = receiveContract.Receiver.Name,
                ReceiverPhoneNumber = receiveContract.Receiver.PhoneNumber,
                CustomerName = contractGroup.CustomerInfo.CustomerName,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
                ModelName = car.CarModel.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,
                DateReceive = receiveContract.DateReceive,
                ReceiveAddress = receiveContract.ReceiveAddress,
                CurrentCarStateSpeedometerNumber = receiveContract.CurrentCarStateSpeedometerNumber,
                CurrentCarStateFuelPercent = receiveContract.CurrentCarStateFuelPercent,
                CurrentCarStateCurrentEtcAmount = receiveContract.CurrentCarStateCurrentEtcAmount,
                CurrentCarStateCarStatusDescription = receiveContract.CurrentCarStateCarStatusDescription,
                CurrentCarStateCarFrontImg = receiveContract.CurrentCarStateCarFrontImg,
                CurrentCarStateCarBackImg = receiveContract.CurrentCarStateCarBackImg,
                CurrentCarStateCarLeftImg = receiveContract.CurrentCarStateCarLeftImg,
                CurrentCarStateCarRightImg = receiveContract.CurrentCarStateCarRightImg,
                CurrentCarStateCarInteriorImg = receiveContract.CurrentCarStateCarInteriorImg,
                CurrentCarStateCarBackSeatImg = receiveContract.CurrentCarStateCarBackSeatImg,
                DepositItemAsset = receiveContract.DepositItemAsset,
                IsExported = receiveContract.IsExported,
                CustomerSignature = receiveContract.CustomerSignature,
                StaffSignature = receiveContract.StaffSignature,
                FilePath = host + receiveContract.FilePath,
                FileWithSignsPath = host + receiveContract.FileWithSignsPath,
                ContractStatusId = receiveContract.ContractStatusId,
            };
        }

        public ReceiveContractDataModel GetReceiveContractByContractGroupId(int contractGroupId)
        {
            ReceiveContract receiveContract = _contractContext.ReceiveContracts
                .Include(c => c.Receiver)
                .FirstOrDefault(c => c.ContractGroupId == contractGroupId);
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .Include(c => c.RentContract)
                .FirstOrDefault(c => c.Id == contractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            return new ReceiveContractDataModel
            {
                Id = receiveContract.Id,
                ContractGroupId = receiveContract.ContractGroupId,
                ReceiverId = receiveContract.ReceiverId,
                ReceiverName = receiveContract.Receiver.Name,
                ReceiverPhoneNumber = receiveContract.Receiver.PhoneNumber,
                CustomerName = contractGroup.CustomerInfo.CustomerName,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,
                ModelName = car.CarModel.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,
                DateReceive = receiveContract.DateReceive,
                ReceiveAddress = receiveContract.ReceiveAddress,
                CurrentCarStateSpeedometerNumber = receiveContract.CurrentCarStateSpeedometerNumber,
                CurrentCarStateFuelPercent = receiveContract.CurrentCarStateFuelPercent,
                CurrentCarStateCurrentEtcAmount = receiveContract.CurrentCarStateCurrentEtcAmount,
                CurrentCarStateCarStatusDescription = receiveContract.CurrentCarStateCarStatusDescription,
                CurrentCarStateCarFrontImg = receiveContract.CurrentCarStateCarFrontImg,
                CurrentCarStateCarBackImg = receiveContract.CurrentCarStateCarBackImg,
                CurrentCarStateCarLeftImg = receiveContract.CurrentCarStateCarLeftImg,
                CurrentCarStateCarRightImg = receiveContract.CurrentCarStateCarRightImg,
                CurrentCarStateCarInteriorImg = receiveContract.CurrentCarStateCarInteriorImg,
                CurrentCarStateCarBackSeatImg = receiveContract.CurrentCarStateCarBackSeatImg,
                DepositItemAsset = receiveContract.DepositItemAsset,

                IsExported = receiveContract.IsExported,
                CustomerSignature = receiveContract.CustomerSignature,
                StaffSignature = receiveContract.StaffSignature,
                FilePath = host + receiveContract.FilePath,
                FileWithSignsPath = host + receiveContract.FileWithSignsPath,
                ContractStatusId = receiveContract.ContractStatusId,
            };
        }

        public bool ReceiveContractExit(int id)
        {
            return _contractContext.ReceiveContracts.Any(c => c.Id == id);
        }

        public void CreateReceiveContract(ReceiveContractCreateModel request)
        {
            var defaultContractId = ContractStatusConstant.ContractExporting;
            string fileName = "ReceiveContract" + ".pdf";

            string htmlContent = CreateReceiveContractContent(request);

            var file = _fileRepository.GeneratePdfAsync(htmlContent, fileName);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());


            var ReceiveContract = new ReceiveContract
            {
                ContractGroupId = request.ContractGroupId,
                ReceiverId = request.ReceiverId,
                DateReceive = request.DateReceive,
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
                DepositItemAsset = request.DepositItemAsset,
                CreatedDate = request.CreatedDate,
                ContractStatusId = defaultContractId,
                FilePath = filePath
            };
            _contractContext.ReceiveContracts.Add(ReceiveContract);
            _contractContext.SaveChanges();

            var contractGroupStatusReceiveNotSign = Constant.ContractGroupConstant.ReceiveContractNotSign;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusReceiveNotSign;

            _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
        }

        public void UpdateReceiveContract(int id, ReceiveContractUpdateModel request)
        {
            string fileName = "ReceiveContract" + ".pdf";

            string htmlContent = UpdateReceiveContractContent(request);

            var file = _fileRepository.GeneratePdfAsync(htmlContent, fileName);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());

            var receiveContract = _contractContext.ReceiveContracts.Find(id);

            receiveContract.ContractGroupId = request.ContractGroupId;
            receiveContract.ReceiverId = request.ReceiverId;
            receiveContract.DateReceive = request.DateReceive;
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
            receiveContract.DepositItemAsset = request.DepositItemAsset;
            receiveContract.IsExported = request.IsExported;
            receiveContract.CustomerSignature = request.CustomerSignature;
            receiveContract.StaffSignature = request.StaffSignature;
            receiveContract.ContractStatusId = request.ContractStatusId;
            receiveContract.FilePath = filePath;

            if (request.CustomerSignature != null && request.StaffSignature != null)
            {
                receiveContract.ContractStatusId = Constant.ContractStatusConstant.ContractExported;
            }
            else
            {
                receiveContract.ContractStatusId = request.ContractStatusId;
            }

            _contractContext.ReceiveContracts.Update(receiveContract);
            _contractContext.SaveChanges();

            if (request.CustomerSignature != null && request.StaffSignature != null)
            {
                var contractGroupStatusReceiveSigned = Constant.ContractGroupConstant.ReceiveContractSigned;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusReceiveSigned;

                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
            }
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
            var Receiveer = _contractContext.Users.FirstOrDefault(c => c.Id == request.ReceiverId);
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            string htmlContent = "<h1 style= " + "color: blue; text - align:center;" + "> CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style= " + "color: blue; text - align:center;" + "> Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h2>BIÊN BẢN GIAO NHẬN XE </h2>";

            htmlContent += "<p>Trước và sau khi cho thuê, kèm theo hợp đồng thuê xe số " + request.ContractGroupId + "</p>";
            htmlContent += "<p>Hôm nay, ngày " + request.DateReceive + ", chúng tôi gồm:</p>";
            htmlContent += "<h2>BÊN CHO THUÊ XE (BÊN A): </h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Đại diện: " + Receiveer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + Receiveer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B):</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
            htmlContent += "<li>Điện thoại người thân:(chỉ dùng trong trường hợp khẩn khi không liên lạc được với bên B): " + contractGroup.CustomerInfo.RelativeTel + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Căn cứ theo Hợp đồng thuê xe đã được ký kết, chúng tôi cùng tiến hành giao nhận xe và lập biên bản với những nội dung sau:</p>";
            htmlContent += "<h2>A.Thông tin xe bàn giao</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Hiệu xe: " + car.CarModel.Name + "</li>";
            htmlContent += "<li>Biển số: " + car.CarLicensePlates + "</li>";
            htmlContent += "<li>Số chỗ: " + car.SeatNumber + "</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>B.Tình trạng xe khi giao xe</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Số công tơ mét (Km): " + request.CurrentCarStateSpeedometerNumber + " Km</li>";
            htmlContent += "<li>Đồng hồ xăng/dầu: " + request.CurrentCarStateFuelPercent + " %</li>";
            htmlContent += "<li>Số dư TK thu phí cao tốc ETC: " + request.CurrentCarStateCurrentEtcAmount + " VNĐ</li>";
            htmlContent += "<li>Xe có một số vấn đề(nội thất, ngoại thất, máy móc…) cần lưu ý sau: " + request.CurrentCarStateCarStatusDescription + "</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>C.  Bên A có giữ của bên B một số giấy tờ và tài sản như sau:</h2>";
            //htmlContent += "<ul>";
            //htmlContent += "<li>Giấy tờ đặt cọc(Sổ tạm trú/Hộ khẩu/ Hộ chiếu, số): " + request.DepositItemPaper + "</li>";
            //htmlContent += "<li>Tài sản đặt cọc: " + request.DepositItemAssetInfo + "</li>";
            //htmlContent += "<li>Tiền mặt, số tiền: " + request.DepositItemAsset + "</li>";
            //htmlContent += "</ul>";
            htmlContent += "<p>Lưu ý:</p>";
            htmlContent += "<ul>";
            htmlContent += "<li>Khi giao xe, Bên A và Bên B nên kiểm tra kĩ tình trạng xe, ghi chú lại bằng biên bản các hư hỏng nếu có và lưu lại hình ảnh tình trạng xe bằng điện thoại (10-15 hình ảnh, chụp cả ngoại thất, nội thất, công tơ mét, đồng hồ xăng dầu) để tránh tranh chấp phát sinh khi hoàn trả.</li>";
            htmlContent += "<li>Trong quá trình sử dụng xe bên B chịu hoàn toàn trách nhiệm dân sự, hình sự, và luật lệ an toàn giao thông trước pháp luật nếu có phát sinh bất cứ chi phí phạt nào tại thời điểm bên B thuê xe, bên B vẫn chịu chi phí đó mặc dù hợp đồng đã kết thúc.</li>";
            htmlContent += "<li>Khi bên A phát hiện bên B các dấu hiệu vi phạm giao thông có thể dẫn đến phạt nguôi như chạy quá tốc độ, đi vào đường cấm, vượt đèn đỏ …bằng các biện pháp nghiệp vụ như theo dõi trên thiết bị định vị GPS, cam hành trình hoặc do bên thứ 3 cung cấp. Bên B có quyền đơn phương chấm dứt hợp đồng luôn với bên A tại thời điểm đó. Giấy tờ và tài sản đặt cọc(tiền hoặc xe máy) mà bên B đặt lại, bên A sẽ có quyền giữ lại cho đến khi xác minh được mức độ vi phạm và chi phí khắc phục lỗi vi phạm.Toàn bộ chi phí khắc phục lỗi vi phạm sẽ do bên B chi trả.</li>";
            htmlContent += "<li>Trong trường hợp bên B chấp hành đầy đủ các quy định về an toàn giao thông và bên A không phát hiện ra lỗi nào thì bên A hoàn trả lại giấy tờ và tài sản đặt cọc cho bên B từ 2 đến 3 ngày làm việc.</li>";
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
            var Receiveer = _contractContext.Users.FirstOrDefault(c => c.Id == request.ReceiverId);
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            string htmlContent = "<h1 style= " + "color: blue; text - align:center;" + "> CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style= " + "color: blue; text - align:center;" + "> Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h2>BIÊN BẢN GIAO NHẬN XE </h2>";

            htmlContent += "<p>Trước và sau khi cho thuê, kèm theo hợp đồng thuê xe số " + request.ContractGroupId + "</p>";
            htmlContent += "<p>Hôm nay, ngày " + request.DateReceive + ", chúng tôi gồm:</p>";
            htmlContent += "<h2>BÊN CHO THUÊ XE (BÊN A): </h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Đại diện: " + Receiveer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + Receiveer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B):</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
            htmlContent += "<li>Điện thoại người thân:(chỉ dùng trong trường hợp khẩn khi không liên lạc được với bên B): " + contractGroup.CustomerInfo.RelativeTel + "</li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Căn cứ theo Hợp đồng thuê xe đã được ký kết, chúng tôi cùng tiến hành giao nhận xe và lập biên bản với những nội dung sau:</p>";
            htmlContent += "<h2>A.Thông tin xe bàn giao</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Hiệu xe: " + car.CarModel.Name + "</li>";
            htmlContent += "<li>Biển số: " + car.CarLicensePlates + "</li>";
            htmlContent += "<li>Số chỗ: " + car.SeatNumber + "</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>B.Tình trạng xe khi giao xe</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Số công tơ mét (Km): " + request.CurrentCarStateSpeedometerNumber + " Km</li>";
            htmlContent += "<li>Đồng hồ xăng/dầu: " + request.CurrentCarStateFuelPercent + " %</li>";
            htmlContent += "<li>Số dư TK thu phí cao tốc ETC: " + request.CurrentCarStateCurrentEtcAmount + " VNĐ</li>";
            htmlContent += "<li>Xe có một số vấn đề(nội thất, ngoại thất, máy móc…) cần lưu ý sau: " + request.CurrentCarStateCarStatusDescription + "</li>";
            htmlContent += "</ul>";
            htmlContent += "<h2>C.  Bên A có giữ của bên B một số giấy tờ và tài sản như sau:</h2>";
            //htmlContent += "<ul>";
            //htmlContent += "<li>Giấy tờ đặt cọc(Sổ tạm trú/Hộ khẩu/ Hộ chiếu, số): " + request.DepositItemPaper + "</li>";
            //htmlContent += "<li>Tài sản đặt cọc: " + request.DepositItemAssetInfo + "</li>";
            //htmlContent += "<li>Tiền mặt, số tiền: " + request.DepositItemAsset + "</li>";
            //htmlContent += "</ul>";
            htmlContent += "<p>Lưu ý:</p>";
            htmlContent += "<ul>";
            htmlContent += "<li>Khi giao xe, Bên A và Bên B nên kiểm tra kĩ tình trạng xe, ghi chú lại bằng biên bản các hư hỏng nếu có và lưu lại hình ảnh tình trạng xe bằng điện thoại (10-15 hình ảnh, chụp cả ngoại thất, nội thất, công tơ mét, đồng hồ xăng dầu) để tránh tranh chấp phát sinh khi hoàn trả.</li>";
            htmlContent += "<li>Trong quá trình sử dụng xe bên B chịu hoàn toàn trách nhiệm dân sự, hình sự, và luật lệ an toàn giao thông trước pháp luật nếu có phát sinh bất cứ chi phí phạt nào tại thời điểm bên B thuê xe, bên B vẫn chịu chi phí đó mặc dù hợp đồng đã kết thúc.</li>";
            htmlContent += "<li>Khi bên A phát hiện bên B các dấu hiệu vi phạm giao thông có thể dẫn đến phạt nguôi như chạy quá tốc độ, đi vào đường cấm, vượt đèn đỏ …bằng các biện pháp nghiệp vụ như theo dõi trên thiết bị định vị GPS, cam hành trình hoặc do bên thứ 3 cung cấp. Bên B có quyền đơn phương chấm dứt hợp đồng luôn với bên A tại thời điểm đó. Giấy tờ và tài sản đặt cọc(tiền hoặc xe máy) mà bên B đặt lại, bên A sẽ có quyền giữ lại cho đến khi xác minh được mức độ vi phạm và chi phí khắc phục lỗi vi phạm.Toàn bộ chi phí khắc phục lỗi vi phạm sẽ do bên B chi trả.</li>";
            htmlContent += "<li>Trong trường hợp bên B chấp hành đầy đủ các quy định về an toàn giao thông và bên A không phát hiện ra lỗi nào thì bên A hoàn trả lại giấy tờ và tài sản đặt cọc cho bên B từ 2 đến 3 ngày làm việc.</li>";
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
                htmlContent += "&nbsp;&nbsp;&nbsp;&nbsp;<img style= 'width:100px; height:100%' src='" + request.CustomerSignature + "' />";
            }

            return htmlContent;
        }
    }
}
