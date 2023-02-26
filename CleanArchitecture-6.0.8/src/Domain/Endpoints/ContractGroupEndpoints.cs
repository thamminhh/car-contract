namespace CleanArchitecture.Domain.Endpoints;

public class ContractGroupEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/contractgroup";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{contractGroupId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string UpdateContractGroupStatus = Base + "/update-status/{id}";
    public const string Delete = Base + "/delete/{id}";
}
