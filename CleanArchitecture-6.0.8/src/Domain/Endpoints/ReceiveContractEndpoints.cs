namespace CleanArchitecture.Domain.Endpoints;

public class ReceiveContractEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string CreateReceiveContract = Base + "/create-receive-contract";
    //public const string UpdateReceiveContract = Base + "/update-receive-contract/{groupId}";
    //public const string DeleteReceiveContract = Base + "/delete-receive-contract/{id}";
    //public const string ExportReceiveCarContract = Base + "/export-receive-contract/{groupId}";

    public const string Area = "";
    public const string Base = Area + "/receivecontract";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByContractGroupId = Base + "/get-by-contractGroupId/{contractGroupId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string UpdateContractStatus = Base + "/update-contract-status/{id}";


}
