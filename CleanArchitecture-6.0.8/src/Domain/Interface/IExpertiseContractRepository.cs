using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ExpertiseContract;

namespace CleanArchitecture.Domain.Interface
{
    public interface IExpertiseContractRepository
    {

        ExpertiseContract GetExpertiseContractById(int id);

        ExpertiseContract GetExpertiseContractByContractGroupId(int contractGroupId);

        void CreateExpertiesContract(ExpertiseContractCreateModel request);

        void UpdateExpertiseContract(int id, ExpertiseContractUpdateModel request);
        bool UpdateExpertiseContractStatus(int id, ExpertiseContractUpdateStatusModel request);

        bool ExpertiseContractExit(int id);  

    }
}
