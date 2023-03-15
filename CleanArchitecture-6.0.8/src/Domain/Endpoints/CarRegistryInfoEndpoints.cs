namespace CleanArchitecture.Domain.Endpoints;

public class CarMaintenanceInfoEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string CreateExpertise = Base + "/create-expertise";
    //public const string UpdateExpertise = Base + "/update-expertise";
    //public const string ExportCarMaintenanceInfo = Base + "/export-expertise-contract/{groupId}";

    public const string Area = "";
    public const string Base = Area + "/carmaintenanceinfo";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarId = Base + "/get-by-carId/{carId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string UpdateContractStatus  = Base + "/update-contract-status/{id}";
}
