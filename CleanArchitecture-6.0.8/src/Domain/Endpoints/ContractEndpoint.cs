namespace CleanArchitecture.Domain.Endpoints;

public class ContractEndpoint
{
    public const string Area = "";
    public const string Base = Area + "/contractgroup";
    public const string GetAll = Base;
    public const string GetByUser = Base + "/user/{id}";
    public const string GetSingle = Base + "/{id}";
    public const string CreateContract = Base + "/create-contractgroup";   
    public const string CreateFile = Base + "/create-file";
    public const string UpdateContract = Base + "/update-contractgroup/{id}";
    public const string UpdateFile = Base + "/update-file/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string Preview = Base + "/preview";
    public const string CustomerSign = Base + "/customer-sign";
    public const string StaffSign = Base + "/staff-sign";
}
