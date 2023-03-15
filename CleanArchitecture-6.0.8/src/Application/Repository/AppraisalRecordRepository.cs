using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.AppraisalRecord;
using CleanArchitecture.Domain.Interface;
using MediatR;
using PdfSharpCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
            var appraisalRecords = _contractContext.AppraisalRecords.Where(c => c.Id == id).FirstOrDefault();
            var contractGroup = _contractContext.ContractGroups.Find(appraisalRecords.ContractGroupId);
            var host = _fileRepository.GetCurrentHost();
            return new AppraisalRecordDataModel
            {
                Id = id,
                ContractGroupId = appraisalRecords.ContractGroupId,
                ExpertiserId = appraisalRecords.ExpertiserId,
                CarId = contractGroup.CarId,
                ExpertiseDate = appraisalRecords.ExpertiseDate,
                ResultOfInfo = appraisalRecords.ResultOfInfo,
                ResultOfCar = appraisalRecords.ResultOfCar,
                ResultDescription = appraisalRecords.ResultDescription,
                DepositInfoDescription = appraisalRecords.DepositInfoDescription,
                DepositInfoAsset = appraisalRecords.DepositInfoAsset,
                FilePath = host + appraisalRecords.FilePath,
                DepositInfoDownPayment = appraisalRecords.DepositInfoDownPayment,
                PaymentAmount = appraisalRecords.PaymentAmount,
            };
        }

        public AppraisalRecordDataModel GetMaxAppraisalRecordByContractGroupId(int carId)
        {
            IQueryable<AppraisalRecord> appraisalRecords = _contractContext.AppraisalRecords
                .Include(c => c.ContractGroup)
                .Where(c => c.ContractGroup.CarId == carId)
                .AsQueryable();

            var host = _fileRepository.GetCurrentHost();

            var maxAppraisalRecord = appraisalRecords
                .OrderByDescending(c => c.Id)
                .Select(c => new AppraisalRecordDataModel
                {
                    Id = c.Id,
                    ContractGroupId = c.ContractGroupId,
                    ExpertiserId = c.ExpertiserId,
                    CarId = c.ContractGroup != null ? c.ContractGroup.CarId : null,
                    ExpertiseDate = c.ExpertiseDate,
                    ResultOfInfo = c.ResultOfInfo,
                    ResultOfCar = c.ResultOfCar,
                    ResultDescription = c.ResultDescription,
                    DepositInfoDescription = c.DepositInfoDescription,
                    DepositInfoAsset = c.DepositInfoAsset,
                    FilePath = host + c.FilePath,
                    DepositInfoDownPayment = c.DepositInfoDownPayment,
                    PaymentAmount = c.PaymentAmount,
                })
                .FirstOrDefault();

            return maxAppraisalRecord;
        }

        public ICollection <AppraisalRecordDataModel> GetAppraisalRecordByContractGroupId(int contractGroupId)
        {

            IQueryable<AppraisalRecord> appraisalRecords = _contractContext.AppraisalRecords
                .Include(c => c.ContractGroup)
                .AsQueryable();

            var host = _fileRepository.GetCurrentHost();

            var appraisalRecordModels = appraisalRecords
                .OrderBy(c => c.Id)
                .Select(c => new AppraisalRecordDataModel
                {
                    Id = c.Id,
                    ContractGroupId = c.ContractGroupId,
                    ExpertiserId = c.ExpertiserId,
                    CarId = c.ContractGroup != null ? c.ContractGroup.CarId : null,
                    ExpertiseDate = c.ExpertiseDate,
                    ResultOfInfo = c.ResultOfInfo,
                    ResultOfCar = c.ResultOfCar,
                    ResultDescription = c.ResultDescription,
                    DepositInfoDescription = c.DepositInfoDescription,
                    DepositInfoAsset = c.DepositInfoAsset,
                    FilePath = host + c.FilePath,
                    DepositInfoDownPayment = c.DepositInfoDownPayment,
                    PaymentAmount = c.PaymentAmount,
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

            string htmlContent = "<h1> Biên bản thẩm định </h1>";
            string fileName = "AppraisalRecord" + ".pdf";

            htmlContent += "<h2> ExpertiserId: " + request.ExpertiserId + "</h2>";
            htmlContent += "<h2> ContractGroupId: " + request.ContractGroupId + "</h2>";
            htmlContent += "<h2> DepositInfoDescription: " + request.DepositInfoDescription + "</h2>";
            htmlContent += "<h2> DepositInfoAsset: " + request.DepositInfoAsset + "</h2>";
            htmlContent += "<h2> DepositInfoDownPayment: " + request.DepositInfoDownPayment + "</h2>";
            htmlContent += "<h2> PaymentAmount: " + request.PaymentAmount + "</h2>";

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
                DepositInfoDescription = request.DepositInfoDescription,
                DepositInfoAsset = request.DepositInfoAsset,
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
            var appraisalRecord = _contractContext.AppraisalRecords.Find(id);
            appraisalRecord.ContractGroupId = request.ContractGroupId;
            appraisalRecord.ExpertiserId = request.ExpertiserId;
            appraisalRecord.ExpertiseDate = request.ExpertiseDate;

            appraisalRecord.DepositInfoDescription = request.DepositInfoDescription;
            appraisalRecord.DepositInfoAsset = request.DepositInfoAsset;
            appraisalRecord.DepositInfoDownPayment = request.DepositInfoDownPayment;

            _contractContext.AppraisalRecords.Update(appraisalRecord);
            _contractContext.SaveChanges();

        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
