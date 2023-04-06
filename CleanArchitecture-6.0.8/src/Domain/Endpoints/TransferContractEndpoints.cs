namespace CleanArchitecture.Domain.Endpoints;

public class TransferContractEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string CreateTransferContract = Base + "/create-transfer-contract";
    //public const string UpdateTransferContract = Base + "/update-transfer-contract/{groupId}";
    //public const string DeleteTransferContract = Base + "/delete-transfer-contract/{id}";
    //public const string ExportTransferCarContract = Base + "/export-transfer-contract/{groupId}";

    public const string Area = "";
    public const string Base = Area + "/transfercontract";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByContractGroupId = Base + "/get-by-contractGroupId/{contractGroupId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string DeleteTransferContractFile = Base + "/delete/{transferContractFileId}";
    public const string UpdateContractStatus = Base + "/update-contract-status/{id}";
}
