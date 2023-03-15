namespace CleanArchitecture.Domain.Endpoints;

public class AppraisalRecordEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string CreateExpertise = Base + "/create-expertise";
    //public const string UpdateExpertise = Base + "/update-expertise";
    //public const string ExportAppraisalRecord = Base + "/export-expertise-contract/{groupId}";

    public const string Area = "";
    public const string Base = Area + "/appraisalrecord";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetLastByContractGroupId = Base + "/contractGroupId/{contractGroupId}";
    public const string GetByContractGroupId = Base + "/get-by-contractGroupId/{contractGroupId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string UpdateContractStatus  = Base + "/update-contract-status/{id}";
}
