using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.AppraisalRecord;
using CleanArchitecture.Domain.Interface;
using MediatR;
using PdfSharpCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNet.SignalR;

namespace CleanArchitecture.Application.Repository
{
    public class AppraisalRecordRepository : IAppraisalRecordRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupController;
        private readonly FileRepository _fileRepository;

        public AppraisalRecordRepository(ContractContext contractContext, IContractGroupRepository contractGroupController, FileRepository fileRepository)
        {
            _contractContext = contractContext;
            _contractGroupController = contractGroupController;
            _fileRepository = fileRepository;
        }

        public AppraisalRecordDataModel GetAppraisalRecordById(int id)
        {
            var host = _fileRepository.GetCurrentHost();

            AppraisalRecord appraisalRecord = _contractContext.AppraisalRecords
                .Include(c => c.ContractGroup)
                .FirstOrDefault(c => c.Id == id);

            ContractGroup contractGroup = _contractContext.ContractGroups
                .Include(c => c.Car)
                .FirstOrDefault(c => c.Id == appraisalRecord.ContractGroupId);

            return new AppraisalRecordDataModel
            {
                Id = id,
                ContractGroupId = appraisalRecord.ContractGroupId,
                ExpertiserId = appraisalRecord.ExpertiserId,
                CarId = contractGroup.Car != null ? contractGroup.CarId : null,
                ExpertiseDate = appraisalRecord.ExpertiseDate,
                ResultOfInfo = appraisalRecord.ResultOfInfo,
                ResultOfCar = appraisalRecord.ResultOfCar,
                ResultDescription = appraisalRecord.ResultDescription,
                FilePath = host + appraisalRecord.FilePath,
                DepositInfoDownPayment = appraisalRecord.DepositInfoDownPayment,
            };
        }

        public AppraisalRecordDataModel GetLastAppraisalRecordByContractGroupId(int contractGroupId)
        {
            var host = _fileRepository.GetCurrentHost();

            var lastRecord = _contractContext.AppraisalRecords
                .Where(ar => ar.ContractGroupId == contractGroupId)
                .OrderByDescending(ar => ar.Id)
                .FirstOrDefault();

            if (lastRecord == null)
            {
                return null;
            }

            var car = _contractContext.ContractGroups
                .Where(cg => cg.Id == contractGroupId)
                .Select(cg => cg.Car)
                .FirstOrDefault();

            var appraisalRecordModel = new AppraisalRecordDataModel
            {
                Id = lastRecord.Id,
                ContractGroupId = lastRecord.ContractGroupId,
                ExpertiserId = lastRecord.ExpertiserId,
                CarId = car != null ? car.Id : null,
                ExpertiseDate = lastRecord.ExpertiseDate,
                ResultOfInfo = lastRecord.ResultOfInfo,
                ResultOfCar = lastRecord.ResultOfCar,
                ResultDescription = lastRecord.ResultDescription,
                FilePath = host + lastRecord.FilePath,
                DepositInfoDownPayment = lastRecord.DepositInfoDownPayment,
            };

            return appraisalRecordModel;
        }
        public ICollection <AppraisalRecordDataModel> GetAppraisalRecordByContractGroupId(int contractGroupId)
        {

            var  appraisalRecords = _contractContext.AppraisalRecords.Where(c => c.ContractGroupId == contractGroupId).ToList();

            var host = _fileRepository.GetCurrentHost();

            ContractGroup contractGroup = _contractContext.ContractGroups
               .Include(c => c.Car)
               .FirstOrDefault(c => c.Id == contractGroupId);

            var appraisalRecordModels = appraisalRecords
                .OrderBy(c => c.Id)
                .Select(c => new AppraisalRecordDataModel
                {
                    Id = c.Id,
                    ContractGroupId = c.ContractGroupId,
                    ExpertiserId = c.ExpertiserId,
                    CarId = contractGroup.Car != null ? contractGroup.CarId : null,
                    ExpertiseDate = c.ExpertiseDate,
                    ResultOfInfo = c.ResultOfInfo,
                    ResultOfCar = c.ResultOfCar,
                    ResultDescription = c.ResultDescription,
                    FilePath = host + c.FilePath,
                    DepositInfoDownPayment = c.DepositInfoDownPayment,
                }).ToList();

                return appraisalRecordModels;
         }


        public bool AppraisalRecordExit(int id)
        {
            return _contractContext.AppraisalRecords.Any(c => c.Id == id);
        }

        public void CreateAppraisalRecord(AppraisalRecordCreateModel request)
        {
            if (request.CarId != null)
            {
                _contractGroupController.UpdateContractCarId(request.ContractGroupId, request.CarId);
            }

            string htmlContent = CreateAppraisalRecordContent(request);
            string fileName = "AppraisalRecord" + ".pdf";

            var file = _fileRepository.GeneratePdfAsync(htmlContent, fileName);

            var filePath = _fileRepository.SaveFileToFolder(file, "1");

            var appraisalRecord = new AppraisalRecord
            {
                ContractGroupId = request.ContractGroupId,
                ExpertiserId = request.ExpertiserId,
                ExpertiseDate = request.ExpertiseDate,
                ResultOfInfo = request.ResultOfInfo,
                ResultOfCar = request.ResultOfCar,
                ResultDescription = request.ResultDescription,
                FilePath = filePath,
                DepositInfoDownPayment = request.DepositInfoDownPayment,
                PaymentAmount = request.PaymentAmount
            };
            _contractContext.AppraisalRecords.Add(appraisalRecord);
            _contractContext.SaveChanges();

            var contractGroupStatus = 1;
            if (request.ResultOfInfo == false && request.ResultOfCar == false) 
            {
                contractGroupStatus = Constant.ContractGroupConstant.FailInfo;
            }
            if(request.ResultOfInfo == true && request.ResultOfCar == false) {
                contractGroupStatus = Constant.ContractGroupConstant.FailCar;
            }
            if(request.ResultOfInfo == true && request.ResultOfCar == true)
            {
                contractGroupStatus = Constant.ContractGroupConstant.ContractGroupExpertised;
            }
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatus;

            _contractGroupController.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

        }

        public void UpdateAppraisalRecord(int id, AppraisalRecordUpdateModel request)
        {
            if (request.CarId != null)
            {
                _contractGroupController.UpdateContractCarId(request.ContractGroupId, request.CarId);
            }

            string htmlContent = UpdateAppraisalRecordContent(request);
            string fileName = "AppraisalRecord" + ".pdf";

            var file = _fileRepository.GeneratePdfAsync(htmlContent, fileName);

            var filePath = _fileRepository.SaveFileToFolder(file, "1");

            var appraisalRecord = new AppraisalRecord
            {
                ContractGroupId = request.ContractGroupId,
                ExpertiserId = request.ExpertiserId,
                ExpertiseDate = request.ExpertiseDate,
                ResultOfInfo = request.ResultOfInfo,
                ResultOfCar = request.ResultOfCar,
                ResultDescription = request.ResultDescription,
                DepositInfoCarRental = request.DepositInfoCarRental,
                DepositInfoDownPayment = request.DepositInfoDownPayment,
            };
            _contractContext.AppraisalRecords.Add(appraisalRecord);
            _contractContext.SaveChanges();

            var contractGroupStatus = 1;
            if (request.ResultOfInfo == false && request.ResultOfCar == false)
            {
                contractGroupStatus = Constant.ContractGroupConstant.FailInfo;
            }
            if (request.ResultOfInfo == true && request.ResultOfCar == false)
            {
                contractGroupStatus = Constant.ContractGroupConstant.FailCar;
            }
            if (request.ResultOfInfo == true && request.ResultOfCar == true)
            {
                contractGroupStatus = Constant.ContractGroupConstant.ContractGroupExpertised;
            }
            var contractGroupUpdateStatusModel = new ContractGroupUpdateStatusModel();
            contractGroupUpdateStatusModel.Id = request.ContractGroupId;
            contractGroupUpdateStatusModel.ContractGroupStatusId = contractGroupStatus;

            _contractGroupController.UpdateContractGroupStatus(request.ContractGroupId, contractGroupUpdateStatusModel);

        }
        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public string CreateAppraisalRecordContent(AppraisalRecordCreateModel request)
        {
            var expertiser = _contractContext.Users.FirstOrDefault(c => c.Id == request.ExpertiserId);

            string htmlContent = "<h1 style='text-align:center;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style='text-align:center;'>Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h1>Biên bản thẩm định </h1>";
            htmlContent += "<h2>Nhân viên thẩm định: </h2>";
            htmlContent += "<p>Hôm nay, ngày " + request.ExpertiseDate +"</p>";

            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ: " + expertiser.CurrentAddress + "</li>";
            htmlContent += "<li>Đại diện: " + expertiser.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + expertiser.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Sau khi kiểm tra thông tin khách hàng và yêu cầu của khách hàng: </p>";
            htmlContent += "<ul>";
            if (request.ResultOfInfo == false)
            {
                htmlContent += "<li>Kết quả thẩm định thông tin: Chưa đạt</li>";
            }
            if (request.ResultOfInfo == true)
            {
                htmlContent += "<li>Kết quả thẩm định thông tin: Đạt</li>";
            }
            if (request.ResultOfCar == false)
            {
                htmlContent += "<li>Kết quả chọn xe: Chưa đạt</li>";
            }
            if (request.ResultOfCar == true)
            {
                htmlContent += "<li>Kết quả chọn xe: Đạt</li>";
            }
            htmlContent += "<li>Mô tả kết quả: " + request.ResultDescription + " </li>"; 
            htmlContent += "</ul>";


            htmlContent += "<p>Thông tin đặt cọc : </p>";
            htmlContent += "<ul>";
            htmlContent += "<li>Số tiền đặt cọc thuê xe: " + request.DepositInfoCarRental + " VNĐ</li>";
            htmlContent += "<li>Số tiền đặt cọc: " + request.DepositInfoDownPayment+ " VNĐ</li>";
            htmlContent += "</ul>";    

            return htmlContent;

        }
        public string UpdateAppraisalRecordContent(AppraisalRecordUpdateModel request)
        {
            var expertiser = _contractContext.Users.FirstOrDefault(c => c.Id == request.ExpertiserId);

            string htmlContent = "<h1 style='text-align:center;'>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h1>";
            htmlContent += "<h1 style='text-align:center;'>Độc lập – Tự do – Hạnh phúc</h1>";
            htmlContent += "<h1>Biên bản thẩm định </h1>";
            htmlContent += "<h2>Nhân viên thẩm định: </h2>";
            htmlContent += "<p>Hôm nay, ngày " + request.ExpertiseDate + "</p>";

            htmlContent += "<ul>";
            htmlContent += "<li>Địa chỉ: " + expertiser.CurrentAddress + "</li>";
            htmlContent += "<li>Đại diện: " + expertiser.Name + "</li>";
            htmlContent += "<li>Điện thoại: " + expertiser.PhoneNumber + " </li>";
            htmlContent += "</ul>";

            htmlContent += "<p>Sau khi kiểm tra thông tin khách hàng và yêu cầu của khách hàng: </p>";
            htmlContent += "<ul>";
            if (request.ResultOfInfo == false)
            {
                htmlContent += "<li>Kết quả thẩm định thông tin: Chưa đạt</li>";
            }
            if (request.ResultOfInfo == true)
            {
                htmlContent += "<li>Kết quả thẩm định thông tin: Đạt</li>";
            }
            if (request.ResultOfCar == false)
            {
                htmlContent += "<li>Kết quả chọn xe: Chưa đạt</li>";
            }
            if (request.ResultOfCar == true)
            {
                htmlContent += "<li>Kết quả chọn xe: Đạt</li>";
            }
            htmlContent += "<li>Mô tả kết quả: " + request.ResultDescription + " </li>";
            htmlContent += "</ul>";


            htmlContent += "<p>Thông tin đặt cọc : </p>";
            htmlContent += "<ul>";
            htmlContent += "<li>Số tiền đặt cọc thuê xe: " + request.DepositInfoCarRental + " VNĐ</li>";
            htmlContent += "<li>Số tiền đặt cọc: " + request.DepositInfoDownPayment + " VNĐ</li>";
            htmlContent += "</ul>";

            return htmlContent;

        }

        //public AppraisalRecordDataModel GetLastAppraisalRecordByContractGroupId(int contractGroupId)
        //{
        //    var host = _fileRepository.GetCurrentHost();

        //    var appraisalRecord = _contractContext.AppraisalRecords.Where(c => c.ContractGroupId == contractGroupId);

        //    ContractGroup contractGroup = _contractContext.ContractGroups
        //        .Include(c => c.Car)
        //        .FirstOrDefault(c => c.Id == contractGroupId);

        //    var appraisalRecordModel = appraisalRecord
        //        .OrderBy(c => c.Id)
        //        .Select(c => new AppraisalRecordDataModel
        //        {
        //            Id = c.Id,
        //            ContractGroupId = c.ContractGroupId,
        //            ExpertiserId = c.ExpertiserId,
        //            CarId = contractGroup.Car != null ? contractGroup.CarId : null,
        //            ExpertiseDate = c.ExpertiseDate,
        //            ResultOfInfo = c.ResultOfInfo,
        //            ResultOfCar = c.ResultOfCar,
        //            ResultDescription = c.ResultDescription,
        //            DepositInfoDescription = c.DepositInfoDescription,
        //            DepositInfoAsset = c.DepositInfoAsset,
        //            FilePath = host + c.FilePath,
        //            DepositInfoDownPayment = c.DepositInfoDownPayment,
        //            PaymentAmount = c.PaymentAmount,
        //        })
        //        .LastOrDefault();

        //    return appraisalRecordModel;

        //    //var host = _fileRepository.GetCurrentHost();

        //    //AppraisalRecord appraisalRecord = _contractContext.AppraisalRecords
        //    //    .Include(c => c.ContractGroup)
        //    //    .LastOrDefault(c => c.ContractGroupId == contractGroupId);

        //    //ContractGroup contractGroup = _contractContext.ContractGroups
        //    //    .Include(c => c.Car)
        //    //    .FirstOrDefault(c => c.Id == appraisalRecord.ContractGroupId);

        //    //return new AppraisalRecordDataModel
        //    //{
        //    //    Id = appraisalRecord.Id,
        //    //    ContractGroupId = appraisalRecord.ContractGroupId,
        //    //    ExpertiserId = appraisalRecord.ExpertiserId,
        //    //    CarId = contractGroup.Car != null ? contractGroup.CarId : null,
        //    //    ExpertiseDate = appraisalRecord.ExpertiseDate,
        //    //    ResultOfInfo = appraisalRecord.ResultOfInfo,
        //    //    ResultOfCar = appraisalRecord.ResultOfCar,
        //    //    ResultDescription = appraisalRecord.ResultDescription,
        //    //    DepositInfoDescription = appraisalRecord.DepositInfoDescription,
        //    //    DepositInfoAsset = appraisalRecord.DepositInfoAsset,
        //    //    FilePath = host + appraisalRecord.FilePath,
        //    //    DepositInfoDownPayment = appraisalRecord.DepositInfoDownPayment,
        //    //    PaymentAmount = appraisalRecord.PaymentAmount,
        //    //};
        //}


        //var appraisalRecords = _contractContext.AppraisalRecords.Where(c => c.Id == id).FirstOrDefault();
        //var contractGroup = _contractContext.ContractGroups.Find(appraisalRecords.ContractGroupId);
        //var host = _fileRepository.GetCurrentHost();
        //return new AppraisalRecordDataModel
        //{
        //    Id = id,
        //    ContractGroupId = appraisalRecords.ContractGroupId,
        //    ExpertiserId = appraisalRecords.ExpertiserId,
        //    CarId = contractGroup.CarId,
        //    ExpertiseDate = appraisalRecords.ExpertiseDate,
        //    ResultOfInfo = appraisalRecords.ResultOfInfo,
        //    ResultOfCar = appraisalRecords.ResultOfCar,
        //    ResultDescription = appraisalRecords.ResultDescription,
        //    DepositInfoDescription = appraisalRecords.DepositInfoDescription,
        //    DepositInfoAsset = appraisalRecords.DepositInfoAsset,
        //    FilePath = host + appraisalRecords.FilePath,
        //    DepositInfoDownPayment = appraisalRecords.DepositInfoDownPayment,
        //    PaymentAmount = appraisalRecords.PaymentAmount,
        //};
    }
}
