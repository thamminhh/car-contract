namespace CleanArchitecture.Domain.Endpoints;

public class ContractFileEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/contractfile";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByContractGroupId = Base + "/get-by-contractGroupId/{contractGroupId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
}
