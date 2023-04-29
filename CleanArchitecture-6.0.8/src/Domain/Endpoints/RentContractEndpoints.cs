namespace CleanArchitecture.Domain.Endpoints;

public class RentContractEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string UpdateRentContract = Base + "/update-rent-contract/{groupId}";
    //public const string DeleteRentContract = Base + "/delete-rent-contract/{id}";
    //public const string ExportRentContract = Base + "/export-rent-contract/{groupId}";

    public const string Area = "";
    public const string Base = Area + "/rentcontract";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetLastByContractGroupId = Base + "/last/contractGroupId/{contractGroupId}";
    public const string GetByContractGroupId = Base + "/get-by-contractGroupId/{contractGroupId}";
    public const string GetRentContractFilesByContractId = Base + "/get-files-by-rentContracId/{rentContractId}";
    public const string Create = Base + "/create";
    public const string CreateRentContractFile = Base + "/create-rent-contract-file";
    public const string Update = Base + "/update/{id}";
    public const string DeleteRentContractFile = Base + "/delete/{rentContractFileId}";
    public const string UpdateContractStatus = Base + "/update-contract-status/{id}";
    //public const string UpdateContractSigned = Base + "/update-contract-signed/{id}";
    public const string UpdateRentContractFile = Base + "/update-rent-contract-files";
}
