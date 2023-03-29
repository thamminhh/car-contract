using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.RentContract;
using CleanArchitecture.Domain.Entities_SubModel.TransferContract;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Repository
{
    public class TransferContractRepository : ITransferContractRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupRepository;
        private readonly FileRepository _fileRepository;
        private readonly ITransferContractFileRepository _transferContractFileRepository;

        public TransferContractRepository(ContractContext contractContext, IContractGroupRepository contractGroupRepository,
            FileRepository fileRepository, ITransferContractFileRepository transferContractFileRepository)
        {
            _contractContext = contractContext;
            _contractGroupRepository = contractGroupRepository;
            _fileRepository = fileRepository;
            _transferContractFileRepository= transferContractFileRepository;
        }

        public TransferContractDataModel GetTransferContractById(int id)
        {
            TransferContract transferContract = _contractContext.TransferContracts
                .Include(c => c.Transferer)
                .FirstOrDefault(c => c.Id == id);
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == transferContract.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var transferContractFiles = _transferContractFileRepository.GetTransferContractFilesByTransferContractId(transferContract.Id);

            return new TransferContractDataModel
            {
                Id = id,
                ContractGroupId = transferContract.ContractGroupId,
                TransfererId = transferContract.TransfererId,

                TransfererName = transferContract.Transferer.Name,
                TransfererPhoneNumber = transferContract.Transferer.PhoneNumber,

                CustomerName = contractGroup.CustomerInfo.CustomerName,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,

                ModelName = car.CarModel.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,

                DateTransfer = transferContract.DateTransfer,
                DeliveryAddress = transferContract.DeliveryAddress,
                CurrentCarStateSpeedometerNumber = transferContract.CurrentCarStateSpeedometerNumber,
                CurrentCarStateFuelPercent = transferContract.CurrentCarStateFuelPercent,
                CurrentCarStateCurrentEtcAmount = transferContract.CurrentCarStateCurrentEtcAmount,
                CurrentCarStateCarStatusDescription = transferContract.CurrentCarStateCarStatusDescription,
                DepositItemDescription = transferContract.DepositItemDescription,
                DepositItemAsset = transferContract.DepositItemAsset,
                DepositItemDownPayment = transferContract.DepositItemDownPayment,
                IsExported = transferContract.IsExported,
                CustomerSignature = transferContract.CustomerSignature,
                StaffSignature = transferContract.StaffSignature,
                FilePath = host + transferContract.FilePath,
                FileWithSignsPath = host + transferContract.FileWithSignsPath,
                ContractStatusId = transferContract.ContractStatusId,
                TransferContractFileDataModels = transferContractFiles,
            };
        }

        public TransferContractDataModel GetTransferContractByContractGroupId(int contractGroupId)
        {
            TransferContract transferContract = _contractContext.TransferContracts
                .Include(c => c.Transferer)
                .FirstOrDefault(c => c.ContractGroupId == contractGroupId);
            var host = _fileRepository.GetCurrentHost();
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == contractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);
            var transferContractFiles = _transferContractFileRepository.GetTransferContractFilesByTransferContractId(transferContract.Id);

            return new TransferContractDataModel
            {
                Id = transferContract.Id,
                ContractGroupId = transferContract.ContractGroupId,
                TransfererId = transferContract.TransfererId,

                TransfererName = transferContract.Transferer.Name,
                TransfererPhoneNumber = transferContract.Transferer.PhoneNumber,

                CustomerName = contractGroup.CustomerInfo.CustomerName,
                CustomerAddress = contractGroup.CustomerInfo.CustomerAddress,
                CustomerPhoneNumber = contractGroup.CustomerInfo.PhoneNumber,
                CustomerCitizenIdentificationInfoNumber = contractGroup.CustomerInfo.CitizenIdentificationInfoNumber,
                CustomerCitizenIdentificationInfoAddress = contractGroup.CustomerInfo.CitizenIdentificationInfoAddress,
                CustomerCitizenIdentificationInfoDateReceive = contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive,

                ModelName = car.CarModel.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,

                DateTransfer = transferContract.DateTransfer,
                DeliveryAddress = transferContract.DeliveryAddress,
                CurrentCarStateSpeedometerNumber = transferContract.CurrentCarStateSpeedometerNumber,
                CurrentCarStateFuelPercent = transferContract.CurrentCarStateFuelPercent,
                CurrentCarStateCurrentEtcAmount = transferContract.CurrentCarStateCurrentEtcAmount,
                CurrentCarStateCarStatusDescription = transferContract.CurrentCarStateCarStatusDescription,
                DepositItemDescription = transferContract.DepositItemDescription,
                DepositItemAsset = transferContract.DepositItemAsset,
                DepositItemDownPayment = transferContract.DepositItemDownPayment,
                IsExported = transferContract.IsExported,
                CustomerSignature = transferContract.CustomerSignature,
                StaffSignature = transferContract.StaffSignature,
                FilePath = host + transferContract.FilePath,
                FileWithSignsPath = host + transferContract.FileWithSignsPath,
                ContractStatusId = transferContract.ContractStatusId,
                TransferContractFileDataModels = transferContractFiles,
            };
        }

        public bool TransferContractExit(int id)
        {
            return _contractContext.TransferContracts.Any(c => c.Id == id);
        }

        public void CreateTransferContract(TransferContractCreateModel request)
        {
            //Create PDF and upload to wwwroot 
            string htmlContent = CreateTransferContractContent(request);
            var file = _fileRepository.GeneratePdfAsync(htmlContent);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());

            //Create TransferContract
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

                DepositItemDescription = request.DepositItemDescription,
                DepositItemAsset = request.DepositItemAsset,
                DepositItemDownPayment = request.DepositItemDownPayment,
                CreatedDate = request.CreatedDate,
                ContractStatusId = defaultContractId,
                FilePath = filePath
            };
            _contractContext.TransferContracts.Add(transferContract);
            _contractContext.SaveChanges();

            var transferContractFiles = request.TransferContractFileCreateModels;
            List<TransferContractFile> transferContractFileList = new List<TransferContractFile>();
            if(transferContractFiles != null && transferContractFiles.Any() )
            {
                foreach (var transferContractFileCreate in transferContractFiles)
                {
                    var transferFile = new TransferContractFile
                    {
                        TransferContractId = transferContract.Id,
                        Title = transferContractFileCreate.Title,
                        DocumentImg = transferContractFileCreate.DocumentImg,
                        DocumentDescription = transferContractFileCreate.DocumentDescription,
                    };
                    transferContractFileList.Add(transferFile);
                }
                _contractContext.TransferContractFiles.AddRange(transferContractFileList);
                _contractContext.SaveChanges();
            }

            //Update contractGroup Status
            var contractGroupStatusTransferNotSign = Constant.ContractGroupConstant.TransferContractNotSign;
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusTransferNotSign;
            _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
        }

        public void UpdateTransferContract(int id, TransferContractUpdateModel request)
        {

            string htmlContent = UpdateTransferContractContent(request);
            var file = _fileRepository.GeneratePdfAsync(htmlContent);
            var filePath = _fileRepository.SaveFileToFolder(file, request.ContractGroupId.ToString());


            var transferContract = _contractContext.TransferContracts.Find(id);
            var transferContractFiles = request.TransferContractFileDataModels;

            transferContract.ContractGroupId = request.ContractGroupId;
            transferContract.TransfererId = request.TransfererId;
            transferContract.DateTransfer = request.DateTransfer;
            transferContract.DeliveryAddress = request.DeliveryAddress;
            transferContract.CurrentCarStateSpeedometerNumber = request.CurrentCarStateSpeedometerNumber;
            transferContract.CurrentCarStateFuelPercent = request.CurrentCarStateFuelPercent;
            transferContract.CurrentCarStateCurrentEtcAmount = request.CurrentCarStateCurrentEtcAmount;
            transferContract.CurrentCarStateCarStatusDescription = request.CurrentCarStateCarStatusDescription;

            transferContract.DepositItemDownPayment = request.DepositItemDownPayment;
            transferContract.DepositItemAsset = request.DepositItemAsset;
            transferContract.DepositItemDescription = request.DepositItemDescription;
            transferContract.IsExported = request.IsExported;
            transferContract.CustomerSignature = request.CustomerSignature;
            transferContract.StaffSignature = request.StaffSignature;
            transferContract.FilePath = filePath;
            if (request.CustomerSignature != null && request.StaffSignature != null)
            {
                transferContract.ContractStatusId = Constant.ContractStatusConstant.ContractExported;
                transferContract.FileWithSignsPath = filePath;
            }
            else
            {
                transferContract.ContractStatusId = request.ContractStatusId;
            }
            if(transferContractFiles != null)
            {
                foreach (var transferContractFile in transferContractFiles)
                {
                    var existingFile = _contractContext.TransferContractFiles.FirstOrDefault(cf => cf.Id == transferContractFile.Id && cf.TransferContractId == transferContract.Id);
                    if(existingFile != null)
                    {
                        existingFile.Title = transferContractFile.Title ?? existingFile.Title;
                        existingFile.DocumentImg = transferContractFile.DocumentImg ?? existingFile.DocumentImg;
                        existingFile.DocumentDescription = transferContractFile.DocumentDescription ?? existingFile.DocumentDescription;
                    }
                    else
                    {
                        _contractContext.TransferContractFiles.Add(new TransferContractFile
                        {
                            TransferContractId = transferContract.Id,
                            Title = transferContractFile.Title,
                            DocumentImg = transferContractFile.DocumentImg,
                            DocumentDescription = transferContractFile.DocumentDescription
                        });
                    }
                }

            }

            _contractContext.TransferContracts.Update(transferContract);
            _contractContext.SaveChanges();


            //Update ContractGroup Status
            if (request.CustomerSignature != null && request.StaffSignature != null)
            {
                var contractGroupStatusTransferSigned = Constant.ContractGroupConstant.TransferContractSigned;
                var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
                contractGroupUpdateStatusModel.Id = request.ContractGroupId;
                contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatusTransferSigned;

                _contractGroupRepository.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);
            }
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

        public string CreateTransferContractContent(TransferContractCreateModel request)
        {
            var transferer = _contractContext.Users.FirstOrDefault(c => c.Id == request.TransfererId);
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            string htmlContent = "<h1 style='text-align:center;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style='text-align:center;'>Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h2>BIÊN BẢN GIAO NHẬN XE </h2>";

            htmlContent += "<p>Trước và sau khi cho thuê, kèm theo hợp đồng thuê xe số " + request.ContractGroupId + "</p>";
            htmlContent += "<p>Hôm nay, ngày " + request.DateTransfer + ", chúng tôi gồm:</p>";
            htmlContent += "<h2>BÊN CHO THUÊ XE (BÊN A): </h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Đại diện: " + transferer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + transferer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B):</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>Ngày cấp: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>Địa chỉ: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
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
            htmlContent += "<ul>";
            //htmlContent += "<li>Giấy tờ đặt cọc(Sổ tạm trú/Hộ khẩu/ Hộ chiếu, số): " + request.DepositItemPaper + "</li>";
            //htmlContent += "<li>Tài sản đặt cọc: " + request.DepositItemAssetInfo + "</li>";
            //htmlContent += "<li>Tiền mặt, số tiền: " + request.DepositItemAsset + "</li>";
            htmlContent += "</ul>";
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
        public string UpdateTransferContractContent(TransferContractUpdateModel request)
        {
            var transferer = _contractContext.Users.FirstOrDefault(c => c.Id == request.TransfererId);
            var contractGroup = _contractContext.ContractGroups
                .Include(c => c.CustomerInfo)
                .FirstOrDefault(c => c.Id == request.ContractGroupId);
            var car = _contractContext.Cars
                .Include(c => c.CarModel)
                .FirstOrDefault(c => c.Id == contractGroup.CarId);

            string htmlContent = "<h1 style='text-align:center;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style='text-align:center;'>Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h2>BIÊN BẢN GIAO NHẬN XE </h2>";

            htmlContent += "<p>Trước và sau khi cho thuê, kèm theo hợp đồng thuê xe số " + request.ContractGroupId + "</p>";
            htmlContent += "<p>Hôm nay, ngày " + request.DateTransfer + ", chúng tôi gồm:</p>";
            htmlContent += "<h2>BÊN CHO THUÊ XE (BÊN A): </h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Đại diện: " + transferer.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + transferer.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<h2>BÊN THUÊ XE (BÊN B):</h2>";
            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ hiện tại: " + contractGroup.CustomerInfo.CustomerAddress + "</li>";
            htmlContent += "<li>Số điện thoại: " + contractGroup.CustomerInfo.PhoneNumber + "</li>";
            htmlContent += "<li>CCCD/ CMND số: " + contractGroup.CustomerInfo.CitizenIdentificationInfoNumber + " </li>";
            htmlContent += "<li>Ngày cấp: " + contractGroup.CustomerInfo.CitizenIdentificationInfoDateReceive + " </li>";
            htmlContent += "<li>Địa chỉ: " + contractGroup.CustomerInfo.CitizenIdentificationInfoAddress + " </li>";
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
            htmlContent += "<ul>";
            //htmlContent += "<li>Giấy tờ đặt cọc(Sổ tạm trú/Hộ khẩu/ Hộ chiếu, số): " + request.DepositItemPaper + "</li>";
            //htmlContent += "<li>Tài sản đặt cọc: " + request.DepositItemAssetInfo + "</li>";
            //htmlContent += "<li>Tiền mặt, số tiền: " + request.DepositItemAsset + "</li>";
            htmlContent += "</ul>";
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
