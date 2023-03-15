using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.AppraisalRecord;

namespace CleanArchitecture.Domain.Interface
{
    public interface IAppraisalRecordRepository
    {

        AppraisalRecordDataModel GetAppraisalRecordById(int id);

        AppraisalRecordDataModel GetMaxAppraisalRecordByContractGroupId(int carId);

        ICollection <AppraisalRecordDataModel> GetAppraisalRecordByContractGroupId(int contractGroupId);

        void CreateAppraisalRecord(AppraisalRecordCreateModel request);

        void UpdateAppraisalRecord(int id, AppraisalRecordUpdateModel request);

        bool AppraisalRecordExit(int id);

    }
}
